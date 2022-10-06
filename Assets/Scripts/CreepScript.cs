using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CreepScript : MonoBehaviour
{
    public short HP;
    [Tooltip("Если скорость равна 10, то крип по прямой преодолеет одну клетку за одну секунду")]
    public float MoveSpeed;
    public short Armor;

    public short Damage;
    public float AttackSpeed;
    public float AttackDistance;

    private NavMeshAgent _navAgent;
    private Vector3 _targetPos;
    private float _lastRecalculatePathTime;
    private bool _attackMode;
    private GameObject _attackTarget;
    private float _lastAttackTime;

    void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _targetPos = GameManager.Instance.CreepsTarget.position;
        _navAgent.speed = MoveSpeed;
        _navAgent.SetDestination(_targetPos);
        //GameManager.Instance.NavMeshController.OnObstacleChange += RecalculatePathWithoutTime;
    }

    //private void RecalculatePathWithoutTime()
    //    => RecalculatePath(true);

    void Update()
    {
        //довести до ума push-подход (на C# events), когда крип пересчитывает путь только при событиях возведения\разрушения баррикады, за отведенное время не удалось
        //так что, оставил постоянный перерасчет пути крипами каждые GameManager.Instance.CreepRecalculatePathPeriod секунд
        RecalculatePath();
        Attack();
    }

    private void RecalculatePath(bool dontCheckTime = false)
    {
        if (dontCheckTime || Time.time - _lastRecalculatePathTime > GameManager.Instance.CreepRecalculatePathPeriod)
        {
            var path = new NavMeshPath();
            _navAgent.CalculatePath(_targetPos, path);
            if (path.status != NavMeshPathStatus.PathComplete)
            {             
                if (_navAgent.remainingDistance > AttackDistance || _attackTarget == null)
                    RecalculatePathToClosestObstacle();             
            }
            else
            {
                _attackTarget = null;
                _navAgent.SetPath(path);
            }          
            _lastRecalculatePathTime = Time.time;
        }      
    }

    private void RecalculatePathToClosestObstacle()
    {
        var obstacles = Physics.OverlapSphere(transform.position, GameManager.Instance.PlayboardDiagonal, 1 << GameManager.Instance.ObstacleLayer)
            .OrderBy(c => Vector3.Distance(c.transform.position, transform.position))
            .ToList();
        
        foreach (var obstacle in obstacles)
        {
            //Если все пути к главной цели перекрыты, то хотим назначить целью ближайшую баррикаду (barricade)
            //Однако, если просто передать в SetDestination() координаты баррикады (barricade.transfort.position), то NavMesh работает не совсем ожидаемым образом 
            //Видимо, это связано с тем, что в таком случае цель для NavMesh Agent будет находиться внутри NavMesh Obstacle и является недостижимой, так как баррикады являются NavMesh Obstacle
            //В итоге, к недостижимой цели крипы могут идти не кратчайшим путём, а длинным
            //Что бы избежать такого поведения, задаем для крипа достижимую цель рядом с ближайшей баррикадой
            //Для этого строим путь к четырём точкам по бокам баррикады (barricadeScript.DestinationPoints), и выбираем достижимый и наиболее короткий путь
            //Если все четыре точки перекрыты, то переходим к следующей по удаленности баррикаде
            //
            var barricadeScript = obstacle.GetComponent<BarricadeScript>();
            var possiblePaths = new NavMeshPath[4].Select(h => new NavMeshPath()).ToArray();
            for (int i = 0; i < 4; i++)
                _navAgent.CalculatePath(barricadeScript.DestinationPoints[i], possiblePaths[i]);

            var (pathLength, path, destination) = possiblePaths
                .Select((possiblePath, index) => (possiblePath, destPoint: barricadeScript.DestinationPoints[index]))
                .Where(x => x.possiblePath.status == NavMeshPathStatus.PathComplete)
                .Select(x => (pathLength: Utils.CalculatePathLength(x.possiblePath), path: x.possiblePath, destination: x.destPoint))
                .DefaultIfEmpty()
                .Min();

            if (path != null)
            {
                _navAgent.SetPath(path);
                _attackTarget = obstacle.gameObject;
                return;
            }
        }

        //Если дошли до этого кода, то значит, что перекрыты пути ко всем баррикадам 
        //По-хорошему, такой ситуации не должно возникать
        _navAgent.isStopped = true;
        _navAgent.ResetPath();
        Debug.Log("Can't find path");
    }

    private void Attack()
    {
        if (_attackTarget != null && Time.time - _lastAttackTime >= 1 / AttackSpeed)
        {
            if (Vector3.Distance(transform.position, _attackTarget.transform.position) <= AttackDistance)
                _attackTarget.GetComponent<BarricadeScript>().TakeDamage(Damage);

            _lastAttackTime = Time.time;
        }
    }

    public bool TakeDamage(short damage)
    {
        var takenDamage = damage - Armor;
        HP -= (short)(takenDamage > 0 ? takenDamage : 0);
        if (HP <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
