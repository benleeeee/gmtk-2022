using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceMovement : MonoBehaviour
{
    [ContextMenu("Reset dice transform")]
    void Reset()
    {
        transform.parent.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);     
    }

    //Need to know the number facing up and the number the player is rolling onto (so that it becomes face down after the roll) to determine the pivot position
    [System.Serializable]
    public struct EdgePivot
    {
        public int numberFacingUp;
        public int numberFacingMoveDir;        
        public Transform pivotPosition;
    }
    public EdgePivot[] _ConnectedFaceEdges;
    private Dictionary<Tuple<int, int>, Transform> _DirToPivotPointsMap = new Dictionary<Tuple<int, int>, Transform>();
    public Transform _Pivot;
    public bool breakOnMove;
    private void Start()
    {
        //Initiailise the dictionary for pivot points
        foreach(var dir in _ConnectedFaceEdges)
        {
            _DirToPivotPointsMap.Add( Tuple.Create(dir.numberFacingUp, dir.numberFacingMoveDir) , dir.pivotPosition);
        }
    }


    public enum E_direction
    {
        up,
        down,
        left,
        right,
        none
    }
    public E_direction _movingDir = E_direction.none;
    bool _PivotSet = false;
    public float _RollSpeed = 1f;
    public float _degreesRotated = 0f; //Resets at end of each roll/movement
    [SerializeField]    
    private int _FaceUpNum = 6;
    void SetPivot()
    {
        //Pick the origin of rotation depending on the direction of moving
        int layermask = 1 << 6;
        int topNum = -1;
        int dirNum = -1;
        RaycastHit topDownHit;
        //Raycast top->down to get up facing number
        if ( Physics.Raycast(transform.position + new Vector3(0,1.0f, 0), Vector3.down, out topDownHit, 2, layermask) )
        {
            topNum = (int)char.GetNumericValue(topDownHit.collider.gameObject.name[topDownHit.collider.gameObject.name.Length - 1]);
        }

        //Raycast side->inwards to get number facing move direction
        RaycastHit sideOnHit;
        Vector3 raycastOffset = Vector3.zero;
        switch(_movingDir)
        {
            case E_direction.up:
                raycastOffset = new Vector3(0, 0, 2);
                break;
            case E_direction.down:
                raycastOffset = new Vector3(0, 0, -2);
                break;
            case E_direction.left:
                raycastOffset = new Vector3(-2, 0, 0);
                break;
            case E_direction.right:
                raycastOffset = new Vector3(2, 0, 0);
                break;
        }
        if (Physics.Raycast(transform.position + raycastOffset, raycastOffset * -1, out sideOnHit, 2, layermask))
        {
            dirNum = (int)char.GetNumericValue(sideOnHit.collider.gameObject.name[sideOnHit.collider.gameObject.name.Length - 1]);
        }

        //Lookup pivot from dictionary
        Tuple<int, int> key = Tuple.Create<int, int>(topNum, dirNum);
        _Pivot = _DirToPivotPointsMap[key];
        _PivotSet = true;        
    }
    
    void Move()
    {
        float rollAmount = _RollSpeed * Time.deltaTime;
        _degreesRotated += rollAmount;        

        //Then roll the player in that direction 90 degrees
        switch (_movingDir)
        {
            case E_direction.up:    //+x
                transform.parent.Rotate(new Vector3(rollAmount, 0, 0));
                break;
            case E_direction.down:  //-x
                transform.parent.Rotate(new Vector3(-rollAmount, 0, 0));
                break;
            case E_direction.left:  //+z
                transform.parent.Rotate(new Vector3(0, 0, rollAmount));
                break;
            case E_direction.right: //-z
                transform.parent.Rotate(new Vector3(0, 0, -rollAmount));
                break;
        }

        //Check player has finished rolling
        if (_degreesRotated >= 90)
        {
            //round each rotation to a multiple of 90
            float exactRotationX = FixToNearestAngle(transform.rotation.x);
            float exactRotationZ = FixToNearestAngle(transform.rotation.z);

            //Reset for next movement input
            ResetForNewMovement();            
        }                
    }
    void MoveTwo()
    {
        float rollAmount = _RollSpeed * Time.deltaTime;
        _degreesRotated += rollAmount;

        //Then roll the player in that direction 90 degrees
        switch (_movingDir)
        {
            case E_direction.up:    //+x
                transform.parent.Rotate(new Vector3( rollAmount, 0, 0));
                break;
            case E_direction.down:  //-x
                transform.parent.Rotate(new Vector3(-rollAmount, 0, 0));
                break;
            case E_direction.left:  //+z
                transform.parent.Rotate(new Vector3(0, 0,  rollAmount));
                break;
            case E_direction.right: //-z
                transform.parent.Rotate(new Vector3(0, 0, -rollAmount));
                break;
        }

        //Check player has finished rolling
        if (_degreesRotated >= 90)
        {
            //round each rotation to a multiple of 90
            float exactRotationX = FixToNearestAngle(transform.rotation.x);
            float exactRotationZ = FixToNearestAngle(transform.rotation.z);
            transform.rotation.eulerAngles.Set(exactRotationX, 0, exactRotationZ);

            //Reset for next movement input
            ResetForNewMovement();

            FindFaceUpNum();
        }
    }
    private int FixToNearestAngle(float angle)
    {        
        return (int)Math.Round(angle / (double)90, MidpointRounding.ToEven) * 90;
    }
    private void ResetForNewMovement()
    {
        _PivotSet = false;
        _degreesRotated = 0;
        _movingDir = E_direction.none;
    }
    private void FindFaceUpNum()
    {
        //Raycast to find up facing numbere
        //RaycastHit hit; 
        Physics.Raycast(transform.position + new Vector3(0, 2, 0), Vector3.down, out RaycastHit hit, 2, 1 << 6);
        Debug.DrawLine (transform.position + new Vector3(0, 2, 0), transform.position, Color.green, 100.0f);

        if (hit.collider != null)
            _FaceUpNum = (int)char.GetNumericValue(hit.collider.gameObject.name[hit.collider.gameObject.name.Length - 1]);
        else
            Debug.LogError("Failed to find dice face with raycast");
    }

    private void Update()
    {
        //Draw down for dice
        //Debug.DrawLine(transform.parent.position, (transform.parent.position - transform.position) * 2.0f, Color.green, 0.1f);
        

        //Only check for player input if not moving
        CheckForInput();        
    }
    private void OnDrawGizmos()
    {
        if (_PivotSet)
            Gizmos.DrawSphere(_Pivot.position, 0.1f);
    }

    void FixedUpdate()
    {
        if (_movingDir != E_direction.none)
        {
            
            MoveTwo();
        }
    }


    void CheckForInput()
    {
        if (_movingDir == E_direction.none)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _movingDir = E_direction.up;
                OnInput();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _movingDir = E_direction.left;
                OnInput();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _movingDir = E_direction.down;
                OnInput();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _movingDir = E_direction.right;
                OnInput();
            }
        }
    }
    void OnInput()
    {
        if (_PivotSet == false)        
            SetPivot();
        
        if (breakOnMove)
            Debug.Break();
    }
}
