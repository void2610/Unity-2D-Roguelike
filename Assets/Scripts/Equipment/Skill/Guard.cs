namespace NEquipment
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using NCharacter;

	public class Guard : Skill
	{
		public void Awake()
		{
			name = "Dash";
			actionKey = "Fire2";
			isCooling = false;
			coolTimeLength = 3.0f;
			isEnable = true;
			isActive = false;
			activeTimeLength = 3f;
		}

		public override void Effect()
		{
			player.GetComponent<Player>().isInvincible = true;
			player.GetComponent<Player>().isMovable = false;
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, player.GetComponent<Rigidbody2D>().velocity.y, 0);
			if (player.transform.localScale.x > 0)
			{
				this.transform.position = player.transform.position + new Vector3(1.5f, -0.4f, 0);
			}
			else
			{
				this.transform.position = player.transform.position + new Vector3(-1.5f, -0.4f, 0);
			}
		}

		public override void OnActionEnd()
		{
			player.GetComponent<Player>().isInvincible = false;
			player.GetComponent<Player>().isMovable = true;
			this.transform.position = new Vector3(0, 0, -10);
		}

		public override void Start()
		{
			this.transform.position = new Vector3(0, 0, -10);
			base.Start();
		}
	}
}
