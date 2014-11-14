/// <summary>
/// Provisoire  
/// </summary>


using UnityEngine;
public class Nourriture:ScriptableObject
{
	private int quantite;
	public Nourriture (int quantite)
	{
		this.quantite = quantite;
	}
	public void diminuerQuantite ()
	{
		this.quantite = this.quantite - 1;
	}
}
