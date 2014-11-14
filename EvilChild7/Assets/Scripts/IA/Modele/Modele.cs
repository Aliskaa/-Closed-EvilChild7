
public abstract class Modele
{
	protected int pointsDattaque;
	protected int mouvement;
	protected int pointsDeVie;
	
	public int getMouvement(){
		return this.mouvement;
	}
	
	public int getPointsDeVie(){
		return this.pointsDeVie;
	}
	
	public void setPointsDeVie(int HP){
		this.pointsDeVie = HP;
	}
	
	public int getAttaque(){
		return this.pointsDattaque;
	}
	
	public int enleverPV(int hp){
		this.setPointsDeVie (this.getPointsDeVie () - hp);
		return pointsDeVie;
	}
	
}