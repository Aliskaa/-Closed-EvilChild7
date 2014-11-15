using UnityEngine;
using System.Collections.Generic;

public class IAscarabee: IAabstraite
{
	protected Scarabee modele;
	protected IAreaction maReaction = null;
	
	public IAscarabee( IAreaction reaction ){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Scarabee();
		maReaction = reaction;
	}
	override public void signaler(List<Cible> objetsReperes){

		if (attaque()) {
			attaquer (attaquants[0]);
		} else {
			List<IAabstraite> ennemisProches = new List<IAabstraite> ();
			List<Cible> ennemisEloignes = new List<Cible> ();
			
			foreach (Cible cible in objetsReperes) {
				if ( cible.getObjet() != null ){
					if (cible.getObjet() is IAabstraite && cible.getType()!=TypesObjetsRencontres.SCARABEE) {
						if (cible.getDistance() <= 1) {
							ennemisProches.Add ((IAabstraite)cible.getObjet ());
						} else {
							ennemisEloignes.Add (cible);
						}
					}
				}
			}
			
			if (ennemisProches.Count > 0) {
				IAabstraite ennemi = ennemisProches[0];
				attaquer (ennemi);
			} else if (ennemisEloignes.Count > 0) {
				bouger (ennemisEloignes [0].getDirection());
			} else {
				maReaction.deambuler ();
			}
		}
	}
	
	void bouger(TypesAxes direction){
		maReaction.bouger(direction,modele.getMouvement());
	}
	
	void deambuler(){
		maReaction.deambuler ();
	}
	
	override public void attaquer(IAabstraite ennemy){
		ennemy.ajouterAttaquant (this);
		if (!victimes.Contains (ennemy)) {
			victimes.Add (ennemy);
		}
		int pvRestants = ennemy.getModele ().enleverPV (modele.getAttaque ());
		if (pvRestants <= 0) {
			ennemy.mort();
		}
	}
	
	override public void mort(){
		foreach (IAabstraite victime in victimes) {
			victime.enleverAttaquant(this);
		}
		
		foreach (IAabstraite attaquant in attaquants) {
			attaquant.enleverVictime(this);
		}
		
		maReaction.mourir ();
	}
	
	override public Modele getModele(){
		return this.modele;
	}
	
	override public TypesCamps getCamp(){
		return TypesCamps.AUCUN;
	}
	
	override public TypesObjetsRencontres retourType(){
		return TypesObjetsRencontres.SCARABEE;
	}
}


