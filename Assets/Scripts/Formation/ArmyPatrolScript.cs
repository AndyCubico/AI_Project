using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ArmyPatrolScript : MonoBehaviour
{
    public GameObject ghostFollower;

    private FormationBase _formation;
    private Vector3 hit = Vector3.zero;
    public FormationBase Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;
    //[SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();
    private Transform _parent;

    private void Awake()
    {
        _parent = new GameObject("Unit Parent").transform;
    }

    private void Update()
    {
        SetFormation();
    }

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count)
        {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count)
        {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++)
        {
            _spawnedUnits[i].GetComponent<NavMeshAgent>().SetDestination(ghostFollower.transform.position);
        }
    }

    private void Spawn(IEnumerable<Vector3> points)
    {
        foreach (var pos in points)
        {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num)
    {
        for (var i = 0; i < num; i++)
        {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}