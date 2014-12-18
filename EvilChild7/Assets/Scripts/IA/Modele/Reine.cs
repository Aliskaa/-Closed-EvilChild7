using UnityEngine;
using System.Collections.Generic;

public class Reine:EntiteApointsDeVie{
	
	private int quantiteNourriture;
	private float ratioOuvriere;
	private float limiteSpeciale = 0.2f;
	
	public static int MAX_OEUFS = 7;
	public static float BAISSER_RATIO = 0.05f;
	public static float AUGMENTER_SPECIAL= 0.02f;
	public static float MAX_SPECIAL= 0.70f;
	public static float MIN_OUV= 0.20f;
	
	public Reine(){
		this.pointsDeVie = 10000;
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
		if (ratio < MIN_OUV) {
			this.limiteSpeciale = MIN_OUV;
		} else {
			this.limiteSpeciale = ratio;
		}
	}
	
	public float getSpeciale(){
		return this.limiteSpeciale;
	}
	
	public void setSpeciale(float ratio){
		if (ratio > MAX_SPECIAL) {
			this.limiteSpeciale = MAX_SPECIAL;
		} else {
			this.limiteSpeciale = ratio;
		}
	}
	
	override public void setPointsDeVie(int HP){
		//Debug.Log("Points de vie reine : "+HP);
		this.pointsDeVie = HP;
	}
}
