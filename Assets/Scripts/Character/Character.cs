namespace NCharacter
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Character : MonoBehaviour
	{
		public string name;

		public int maxHp;

		public int hp;

		public int atk;

		public int killScore;

		public int direction = 1; //-1 = 左  1 = 右

		public Vector2 firstLScale;

		public bool isInvincible = false;

		//相手のHPを減らす機能
		public void CutHP(Character target)
		{
			if (target.isInvincible == false)
			{
				target.hp -= atk;
				Debug.Log(target.name + "のHPが" + atk + "削れた");
				if (target.name == "Player")
				{
					StartCoroutine(StartDamageCooldown(target));
				}
			}

		}

		public IEnumerator StartDamageCooldown(Character target)
		{
			target.isInvincible = true;
			Debug.Log("無敵");
			yield return new WaitForSeconds(0.5f);
			target.isInvincible = false;
		}

		public Character SearchCharacter(GameObject target)
		{
			Character result = null;

			if (target.GetComponent<Character>() != null)
			{
				result = target.GetComponent<Character>();
			}

			return result;
		}

		public virtual void Awake()
		{
			name = "NoName";
			hp = 1;
			atk = 1;
			killScore = 1;
		}

		public virtual void Start()
		{
			firstLScale = this.gameObject.transform.localScale;
		}

		public virtual void Update()
		{
			if (direction == 1)
			{
				this.gameObject.transform.localScale = new Vector2(firstLScale.x, firstLScale.y);
			}
			else
			{
				this.gameObject.transform.localScale = new Vector2(-firstLScale.x, firstLScale.y);
			}
		}

		public virtual void FixedUpdate()
		{
		}
	}
}
