using UnityEngine;
using System.Collections;

public class Cible{
	float distance;
	IAobjet iaObject;
	TypesObjetsRencontres typeObjet;
	public TypesAxes direction;
	GameObject gameObjectCible;

	public Cible(float distance, IAobjet objet, TypesAxes direction, TypesObjetsRencontres typeObjet){
		this.distance = distance;
		this.iaObject = objet;
		this.direction = direction;
		this.typeObjet = typeObjet;
		gameObjectCible = null;
	}

	public Cible(float distance, GameObject objet, TypesAxes direction, TypesObjetsRencontres typeObjet){
		this.distance = distance;
		this.iaObject = null;
		this.direction = direction;
		this.typeObjet = typeObjet;
		gameObjectCible = objet;

	}
	
	public float getDistance(){
		return this.distance;
	}
	
	public IAappel getMonIAappel(){
		IAappel tmp = null;
		tmp = iaObject as IAappel;
		return tmp;
		
	}
	
	public IAobjet getMonIAobjet(){
		return this.iaObject;
		
	}
	
	public IAabstraite getMonIAabstraite(){
		IAabstraite tmp = null;
		tmp = iaObject as IAabstraite;
		return tmp;
		
	}

	public GameObject getGameObject(){
		return gameObjectCible;
	}
	
	public TypesObjetsRencontres getType(){
		return this.typeObjet;
	}
	public TypesAxes getDirection(){
		return this.direction;
	}
}
