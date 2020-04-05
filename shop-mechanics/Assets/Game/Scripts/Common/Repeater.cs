using UnityEngine;

public class Repeater
{
	public float Rate = 0.25f;
	float Next;

	public void Update ()
	{
		float currentTime = 0;
		
        if (Time.time > Next)
        {
            currentTime = Time.time + (Rate);

            if(currentTime > Next)
            Debug.Log(Next);
        }
	}
}
