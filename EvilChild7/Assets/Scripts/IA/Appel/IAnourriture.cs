using System;
public class IAnourriture:IAobjet
{
	protected Nourriture modele;
	protected IAreaction maReaction;
	
	public IAnourriture (IAreaction maReaction)
	{
		modele = new Nourriture ();
		this.maReaction = maReaction;
	}
	
	public void prendreNourriture(){
		modele.diminuerQuantiteNourriture();
		if(modele.getQuantiteNourriture() <= 0){
			maReaction.mourir ();
		}
	}
}


