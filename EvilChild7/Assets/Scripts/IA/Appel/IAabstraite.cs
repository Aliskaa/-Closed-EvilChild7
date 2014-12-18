using UnityEngine;
using System.Collections.Generic;
///<summary>
/// Classe utilisee par le partie en relation directe avec le terrain de l'IA
///</summary>
public abstract class IAabstraite:IAappel{
	
	protected List<IAabstraite> attaquants;
	protected List<IAabstraite> victimes;
	
	public void ajouterAttaquant(IAabstraite attaquant){
		
		if(!this.attaquants.Contains(attaquant)){
			
			this.attaquants.Add(attaquant);
			
		}
	}
	
	public void enleverAttaquant(IAabstraite attaquant){
		if(this.attaquants.Contains(attaquant)){
			this.attaquants.Remove(attaquant);
		}
	}
	public void ajouterVictime(IAabstraite victime){
		if(!this.victimes.Contains(victime)){
			this.victimes.Add(victime);
		}
	}
	
	public void enleverVictime(IAabstraite victime){
		if(this.victimes.Contains(victime)){
			this.victimes.Remove(victime);
		}
	}
	
	public bool attaque(){
		if (this.attaquants.Count > 0) {
			return true;
		}
		return false;
	}
	public IAabstraite getAttaquantAt(int index){
		if (this.attaquants.Count > index) {
			return this.attaquants[index];
		}
		return null;
	}
	
	public List<IAabstraite> getAttaquants(){
		return this.attaquants;
	}
	
	protected bool hasMethod(object objectToCheck, string methodName)
	{
		var type = objectToCheck.GetType();
		return type.GetMethod(methodName) != null;
	}

	//public abstract void attaquer (IAabstraite ennemy);
	
	public abstract void mort();
	
	public abstract void signaler(List<Cible> objetsReperes);
	public abstract EntiteApointsDeVie getModele();
	public abstract TypesCamps getCamp();
	public abstract TypesObjetsRencontres retourType();
}

