using UnityEngine;

public class Cat : MonoBehaviour
{

    public Animator Animator;

	public void Startle()
	{
		Animator.SetTrigger("Startle");
	}
}