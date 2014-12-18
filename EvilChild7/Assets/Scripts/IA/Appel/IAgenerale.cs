using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class IAgenerale:IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Generale modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	
	
	public IAgenerale(TypesObjetsRencontres monType, IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Generale();
		if (monType == TypesObjetsRencontres.GENERALE_BLANCHE) {
			this.monType = TypesObjetsRencontres.GENERALE_BLANCHE;
			monCamp = TypesCamps.BLANC;
		} else {
			monCamp = TypesCamps.NOIR;
			this.monType = TypesObjetsRencontres.GENERALE_NOIRE;
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
				if ( cible.getMonIAabstraite() != null ){
					if (cible.getMonIAabstraite().getCamp()!= getCamp ()) {
						if (cible.getDistance() <= DeplacementsFourmisScript.DISTANCE_CASE) {
							ennemisProches.Add (cible.getMonIAabstraite());
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
	private IAabstraite prioriser(List<IAabstraite> mesEnnemis){
		
		List<IAabstraite> listeScarabees = new List<IAabstraite>();
		List<IAabstraite> listeGenerales = new List<IAabstraite>();
		List<IAabstraite> listeOuvrieres = new List<IAabstraite>();
		List<IAabstraite> listeContremaitre = new List<IAabstraite>();
		List<IAabstraite> listeSoldat = new List<IAabstraite>();
		
		foreach (IAabstraite cible in mesEnnemis) {
			
			if(cible.retourType() == TypesObjetsRencontres.SCARABEE){
				listeScarabees.Add(cible);
			}
			
			if(this.monCamp == TypesCamps.BLANC){
				
				if(cible.retourType() == TypesObjetsRencontres.REINE_NOIRE){
					return cible;
				}
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
				
				if(cible.retourType() == TypesObjetsRencontres.REINE_BLANCHE){
					return cible;
					
				}
				
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
		
		if (listeScarabees.Count > 0) {
			return listeScarabees[0];
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
	
	/*override*/ public void attaquer(IAabstraite ennemy){
		ennemy.ajouterAttaquant (this);
		if (!victimes.Contains (ennemy)) {
			victimes.Add (ennemy);
		}
		
		infligerDegats(ennemy);
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
	
	override public EntiteApointsDeVie getModele(){
		return this.modele;
	}
	
	override public TypesCamps getCamp(){
		return this.monCamp;
	}
	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}
	
	protected void infligerDegats(IAabstraite ennemy){
		int compteurAllies = 0;
		foreach (IAabstraite attaquant in ennemy.getAttaquants()){
			if(attaquant.getCamp() == getCamp ()){
				compteurAllies++;
			}
		}
		int PVRestants = ennemy.getModele ().enleverPV (modele.getAttaque ()*compteurAllies);
		if (PVRestants <= 0) {
			ennemy.mort ();
		}
	}
	
}