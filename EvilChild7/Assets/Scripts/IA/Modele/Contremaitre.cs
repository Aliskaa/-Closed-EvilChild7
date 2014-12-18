using UnityEngine;
using System.Collections.Generic;

public class Contremaitre:Modele{
	
	protected bool nourriture;
	
	public Contremaitre(){
		this.nourriture = false;
		this.pointsDattaque = 1;
		this.mouvement = 1;
		this.pointsDeVie = 50;
	}
	
	public bool transporteNourriture(){
		return this.nourriture;
	}
	
	public void nourriturePrise(){
		if(!this.transporteNourriture()){
			this.nourriture = true;
		}
	}
	
	public void nourriturePosee(){
		if(this.transporteNourriture()){
			this.nourriture = false;
		}
	}
	
}