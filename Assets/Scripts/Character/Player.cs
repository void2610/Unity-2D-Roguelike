namespace NCharacter
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using NManager;
	using UnityEngine;

	public class Player : Character
	{
		public IntVariable hpSO;
		public GameEvent deathEvent;
		public float speed;

		public float jumpForce = 500f;

		public int jp = 0;

		public bool isMovable = true;

		private Rigidbody2D rb;

		private Vector2 speedLimit = new Vector2(5, 30);

		Animator animator;

		JumpResetScript jrs;

		public void Awake()
		{
			base.Awake();
			name = "Player";
			maxHp = PlayerPrefs.GetInt("PlayerMaxHp", 10);
			hp = PlayerPrefs.GetInt("PlayerHp", 10);
			atk = 1;
		}

		public override void Start()
		{
			base.Start();

			rb = this.GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			jrs = GameObject.Find("Leg").GetComponent<JumpResetScript>();
			direction = 1;
		}

		public override void Update()
		{
			base.Update();
			hpSO.RuntimeValue = hp;
			if (hp <= 0)
			{
				deathEvent.Raise();
			}
			jp = jrs.jumpCount;
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
			{
				if (this.jp < 2)
				{
					this.rb.AddForce(transform.up * jumpForce);
					jrs.jumpCount++;
				}
			}
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			if (isMovable)
			{
				//右入力で右向きに動く
				if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
				{
					direction = 1;
					if (Math.Abs(rb.velocity.x) < 100000)
					{
						rb.velocity = new Vector2(speed * direction, rb.velocity.y);
					}
				}
				else
				{
					if (this.jp < 1)
					{
						animator.SetInteger("PlayerState", 0);
					}
				}

				//左入力で左向きに動く
				if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
				{
					direction = -1;
					if (Math.Abs(rb.velocity.x) < 100)
					{
						rb.velocity = new Vector2(speed * direction, rb.velocity.y);
					}
				}
				else
				{
					if (this.jp < 1)
					{
						animator.SetInteger("PlayerState", 0);
					}
				}
				if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}

				if (MathF.Abs(this.GetComponent<Rigidbody2D>().velocity.x) > 0.1f)
				{
					if (this.jp < 1)
					{
						animator.SetInteger("PlayerState", 1);
					}
				}
			}
		}
	}
}
