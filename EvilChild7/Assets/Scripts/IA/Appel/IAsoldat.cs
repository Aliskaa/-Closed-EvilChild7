using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class IAsoldat:IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Soldat modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;


	public IAsoldat(TypesObjetsRencontres monType, IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Soldat();
		if (monType == TypesObjetsRencontres.COMBATTANTE_BLANCHE) {
			this.monType = TypesObjetsRencontres.COMBATTANTE_BLANCHE;
			monCamp = TypesCamps.BLANC;
		} else {
			monCamp = TypesCamps.NOIR;
			this.monType = TypesObjetsRencontres.COMBATTANTE_NOIRE;
		}
		maReaction = reaction;
	}
	override public void signaler(List<Cible> objetsReperes){

		if (attaque ()) {
			attaquer (prioriser (attaquants));
		} else {
			List<IAabstraite> ennemisProches = new List<IAabstraite> ();
			List<Cible> ennemisEloignes = new List<Cible> ();
			
			foreach (Cible cible in objetsReperes) {
				if ( cible.getObjet() != null ){
					if (((IAabstraite)cible.getObjet()).getCamp()!=getCamp ()) {
						if (cible.getDistance() <= 1*DeplacementsFourmisScript.DISTANCE_CASE) {
							ennemisProches.Add ((IAabstraite)cible.getObjet());
						} else {
							ennemisEloignes.Add (cible);
						}
					}
				}
			}
			
			if (ennemisProches.Count > 0) {
				IAabstraite ennemi = prioriser (ennemisProches);
				attaquer (ennemi);
			} else if (ennemisEloignes.Count > 0) {
				bouger (ennemisEloignes [0].getDirection());
			} else {
				//maReaction.bouger(TypesAxes.DEVANT, 1);///////////////////////:
				maReaction.deambuler();
			}
		}
	}
	
	void bouger(TypesAxes direction){
		maReaction.bouger(direction,modele.getMouvement());
	}
	
	void deambuler(){
		maReaction.deambuler ();
	}
	private IAabstraite prioriser(List<IAabstraite> mesEnnemis){
		
		List<IAabstraite> listeGenerales = new List<IAabstraite>();
		List<IAabstraite> listeOuvrieres = new List<IAabstraite>();
		List<IAabstraite> listeContremaitre = new List<IAabstraite>();
		List<IAabstraite> listeSoldat = new List<IAabstraite>();
		
		foreach (IAabstraite cible in mesEnnemis) {
			if(cible.retourType() == TypesObjetsRencontres.SCARABEE){
				return cible;
			}
			
			if(this.monCamp == TypesCamps.BLANC){
				
				if(cible.retourType() == TypesObjetsRencontres.GENERALE_NOIRE){
					listeGenerales.Add (cible);
				}else if(cible.retourType() == TypesObjetsRencontres.COMBATTANTE_NOIRE){
					listeSoldat.Add (cible);
				}else if(cible.retourType() == TypesObjetsRencontres.CONTREMAITRE_NOIRE){
					listeContremaitre.Add (cible);
				}else{
					listeOuvrieres.Add(cible);
				}
			}else{
				if(cible.retourType() == TypesObjetsRencontres.GENERALE_BLANCHE){
					listeGenerales.Add (cible);
				}else if(cible.retourType() == TypesObjetsRencontres.COMBATTANTE_BLANCHE){
					listeSoldat.Add (cible);
				}else if(cible.retourType() == TypesObjetsRencontres.CONTREMAITRE_BLANCHE){
					listeContremaitre.Add (cible);
				}else{
					listeOuvrieres.Add(cible);
				}
			}
		}
		
		if (listeGenerales.Count > 0) {
			return listeGenerales[0];
		} 
		if (listeSoldat.Count > 0) {
			return listeSoldat[0];	
		} 
		if (listeContremaitre.Count > 0) {
			return listeContremaitre[0];	
		} 
		return listeOuvrieres[0];

	}
	
	override public void attaquer(IAabstraite ennemy){

		ennemy.ajouterAttaquant (this);
		if (!victimes.Contains (ennemy)) {
			victimes.Add (ennemy);
		}
		
		// implémentation attauqe en groupe 
		int compteurAllies = 0;
		foreach (IAabstraite attaquant in ennemy.getAttaquants()){
			if((attaquant.getCamp() != TypesCamps.AUCUN) && (attaquant != this)){
				compteurAllies++;
			}
		}
		int pvRestants = ennemy.getModele ().enleverPV (modele.getAttaque () + modele.getAttaque ()*compteurAllies);
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
		return this.monCamp;
	}
	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}
	
}



