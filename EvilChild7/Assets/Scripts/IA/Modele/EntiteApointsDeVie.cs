using UnityEngine;

public abstract class EntiteApointsDeVie
{
	protected int pointsDeVie;
	
	public int enleverPV(int hp){
		this.setPointsDeVie (this.getPointsDeVie () - hp);
		return this.getPointsDeVie ();
	}
	
	public int getPointsDeVie(){
		return this.pointsDeVie;
	}
	
	virtual public void setPointsDeVie(int HP){
		this.pointsDeVie = HP;
	}
}

