using UnityEngine;
using System.Collections;

public class Cible{
	float distance;
	IAappel objet;
	TypesObjetsRencontres typeObjet;
	TypesAxes direction;
	
	public Cible(float distance, IAappel objet, TypesAxes direction, TypesObjetsRencontres typeObjet){
		this.distance = distance;
		this.objet = objet;
		this.direction = direction;
		this.typeObjet = typeObjet;
	}
	
	public float getDistance(){
		return this.distance;
	}
	
	public IAappel getObjet(){
		return this.objet;
	}
	
	public TypesObjetsRencontres getType(){
		return this.typeObjet;
	}
	public TypesAxes getDirection(){
		return this.direction;
	}
}
