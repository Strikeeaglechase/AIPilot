using UnityGERunner;
using Coroutine;

namespace UnityGERunner.UnityApplication
{
	
	public class FuelTank : MonoBehaviour, IMassObject
	{
	    public float maxFuel;
	    public float currentFuel;
	    public float fuelDensity;
	    public float fuelFraction => currentFuel / maxFuel;
	
	    public AIClient aiClient;
	    private bool hasDestroyed = false;
	
	    public void SetFuel(float fuel)
	    {
	        currentFuel = Mathf.Min(maxFuel, Mathf.Max(0, fuel));
	    }
	
	    public void DrawFuel(float amount)
	    {
	        currentFuel -= amount;
	
	        if (currentFuel <= 0 && !hasDestroyed)
	        {
	            aiClient.OnDamage();
	            hasDestroyed = true;
	        }
	    }
	
	    public float GetMass()
	    {
	        return currentFuel * fuelDensity;
	    }
	}
	
}