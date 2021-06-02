using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce= 5.0f;
    private bool _resetJump=false;
    [SerializeField]
    private float _speed =5.0f;
    private bool _grounded=false;
    private PlayerAnimation _playerAnim;
	private SpriteRenderer _spritePlayer;
    void Start()
    {	
		_rigid=GetComponent<Rigidbody2D>();
		_playerAnim = GetComponent<PlayerAnimation>();
		_spritePlayer=GetComponentInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
		Movement();
		//Attack();
        
    }
	void Movement() {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
        if (move == 0.0f) move = Input.GetAxis("Horizontal");
        _grounded = IsGrounded();
        Debug.Log(IsGrounded());
      if (/*Input.GetButtonDown("Boton_A") && IsGrounded() == true || */Input.GetKeyDown(KeyCode.Space) /*&& IsGrounded() == true || CrossPlatformInputManager.GetButtonDown("Boton_A")&& IsGrounded()== true*/ ){
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(ResetJumpRoutine());
          _playerAnim.Jump(true);
        }
      if(move>0){Flip(true);}else if(move<0){Flip(false);}
        _rigid.velocity=new Vector2(move*_speed, _rigid.velocity.y);
         _playerAnim.Move(move);

    }
	bool IsGrounded(){
      RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down,0.1f, 1 << 8 );
      Debug.DrawRay(transform.position, Vector2.down*0.1f, Color.green);
      if(hitInfo.collider != null){
        if(_resetJump==false){
                _playerAnim.Jump(false);
                return true;
        }
      }

      return false;
    }
	void Flip(bool faceRigth){
      if(faceRigth==true){
        _spritePlayer.flipX=false;
       
      
      }
      else if(faceRigth==false){
          _spritePlayer.flipX=true;
        
         
      }
    }
	IEnumerator ResetJumpRoutine(){
      _resetJump=true;
      yield return new WaitForSeconds(0.1f);
      _resetJump = false;
    }
}
