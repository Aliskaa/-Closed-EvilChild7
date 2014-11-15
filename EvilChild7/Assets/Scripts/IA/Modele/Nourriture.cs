using System;
public class Nourriture
{
	private int quantiteNourriture;
	
	public Nourriture ()
	{
		Random random = new Random();
		quantiteNourriture = random.Next (0, 10);
	}
	
	public int getQuantiteNourriture(){
		return this.quantiteNourriture;
	}
	
	public void diminuerQuantiteNourriture(){
		this.quantiteNourriture = this.quantiteNourriture - 1;
	}
}
