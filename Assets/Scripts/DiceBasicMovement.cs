using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DiceBasicMovement : MonoBehaviour
{
    public AudioClip WalkSound;
    public AudioSource WalkSoundSource;
    public float walkSoundCooldown = 0.1f;
    public float walkSoundCDTimer;
    public AudioClip ChipSound;
    public AudioSource ChipsSoundSource;
    public TextMeshProUGUI ChipCounter;
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
            AddChips(25);
            ChipsSoundSource.PlayOneShot(ChipSound);
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
    public void AddChips(int amount)
    {
        _Chips += amount;
        ChipCounter.text = _Chips.ToString();
    }
    private void FixedUpdate()
    {      
        _rb.velocity = _movingDir.normalized * _moveSpeed * Time.deltaTime;

        walkSoundCDTimer -= Time.deltaTime;
        if (walkSoundCDTimer <= 0)
        {
            if (_rb.velocity.sqrMagnitude > 0)
            {
                WalkSoundSource.PlayOneShot(WalkSound);
                walkSoundCDTimer = walkSoundCooldown;
            }
        }
    }
}
