using UnityEngine;
using System.Collections;

public class Vent{

	private static int MAX_PUISSANCE = 5;
	private static int MIN_PUISSANCE = 0;
	private TypesAxes[] axes = new TypesAxes[6]{TypesAxes.DEVANT,TypesAxes.DEVANT_DROITE,TypesAxes.DERRIERE_DROITE,TypesAxes.DERRIERE,TypesAxes.DERRIERE_GAUCHE,TypesAxes.DEVANT_GAUCHE};

	private int puissance;
	private TypesAxes direction;

	public Vent(){

		System.Random random = new System.Random ();
		puissance = random.Next (MIN_PUISSANCE, MAX_PUISSANCE);
		direction = axes[random.Next (0, axes.Length - 1)];
	}

	public TypesAxes getDirection(){
		return this.direction;
	}

	public int getPuissance(){
		return this.puissance;
	}

	public void reroll(){
		System.Random random = new System.Random ();
		this.puissance = random.Next (MIN_PUISSANCE, MAX_PUISSANCE);
		this.direction = axes[random.Next (0, axes.Length - 1)];
	}
}
