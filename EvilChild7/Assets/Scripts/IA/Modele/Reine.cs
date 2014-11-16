using UnityEngine;
using System.Collections.Generic;

public class Reine:EntiteApointsDeVie{
	
	private int quantiteNourriture;
	private float ratioOuvriere;
	
	public Reine(){
		this.pointsDeVie = 1000;
		this.quantiteNourriture = 2;
		this.ratioOuvriere = 0.8f;
	}
	
	public int getQuantiteNourriture(){
		return this.quantiteNourriture;
	}
	
	public void setQuantiteNourriture(int quantite){
		this.quantiteNourriture = quantite;
	}
	
	public void augmenterQuantiteNourriture(){
		this.setQuantiteNourriture (this.getQuantiteNourriture() + 1);
	}
	
	public void augmenterQuantiteNourriture(int quantite){
		this.setQuantiteNourriture (this.getQuantiteNourriture() + quantite);
	}
	
	public float getRatioOuvriere(){
		return this.ratioOuvriere;
	}
	
	public float getRatioSoldat(){
		return 1 - this.ratioOuvriere;
	}
	
	public void setRatioOuvriere(float ratio){
		this.ratioOuvriere = ratio;
	}
	
}
