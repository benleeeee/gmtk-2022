using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceBasicMovement : MonoBehaviour
{
    public int _Chips = 0;
    [SerializeField]
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public enum E_direction
    {
        up,
        down,
        left,
        right,
        none
    }
    [SerializeField]
    private Vector3 _movingDir = Vector3.zero;
    public float _moveSpeed;
    
    // Update is called once per frame
    void Update()
    {
        CheckForInput();        
    }
    void CheckForInput()
    {
        _movingDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _movingDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movingDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _movingDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _movingDir += new Vector3(1, 0, 0);
        }        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Chips"))
        {
            GameObject.Destroy(collision.gameObject);
            _Chips += 25;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _movingDir = Vector3.zero;
            _rb.velocity = Vector3.zero;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Transition to combat scene
            GameObject.Destroy(collision.gameObject);
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Additive);
        }
    }
    private void FixedUpdate()
    {      
        _rb.velocity = _movingDir.normalized * _moveSpeed * Time.deltaTime;
    }
}
