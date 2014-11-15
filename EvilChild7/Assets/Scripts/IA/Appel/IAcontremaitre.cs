using UnityEngine;
using System.Collections.Generic;

public class IAcontremaitre: IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Contremaitre modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	
	public IAcontremaitre(TypesObjetsRencontres monType, IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Contremaitre();
		if (monType == TypesObjetsRencontres.CONTREMAITRE_BLANCHE) {
			monCamp = TypesCamps.BLANC;
			this.monType = TypesObjetsRencontres.CONTREMAITRE_BLANCHE;
		} else {
			monCamp = TypesCamps.NOIR;
			this.monType = TypesObjetsRencontres.CONTREMAITRE_NOIRE;
		}
		maReaction = reaction;
		maReaction.poserPheromones(true);
	}
	override public void signaler(List<Cible> objetsReperes){
		
		if(attaque()) {
			
			attaquer(getAttaquantAt(0));
			
		}else{
			
			if (modele.transporteNourriture()) {
				
				Cible reine = repererReine(objetsReperes);
				
				if(reine!=null){
					
					modele.nourriturePosee();
					
				}else{
					
					rentrerBase();
					
				}
				
			}else{
				
				Cible objetNourriture = repererNourriture(objetsReperes);
				
				if(objetNourriture != null){
					
					if(objetNourriture.getDistance()<=DeplacementsFourmisScript.DISTANCE_CASE){
						
						modele.nourriturePrise();
						prendreNourriture((IAnourriture)objetNourriture.getMonIAobjet());
						
					}else{
						
						bouger (objetNourriture.getDirection());
						
					}
				}else{
					deambuler();
				}
			}
		}
		
	}
	
	void bouger(TypesAxes direction){
		maReaction.bouger(direction,modele.getMouvement());
	}
	
	void deambuler(){
		maReaction.deambuler ();
	}
	
	void rentrerBase(){
		maReaction.rentrerBase ();
	}
	
	/*override*/ public void attaquer(IAabstraite ennemy){
		ennemy.ajouterAttaquant (this);
		if (!victimes.Contains (ennemy)) {
			victimes.Add (ennemy);
		}
		infligerDegats(ennemy);
		
	}
	
	private Cible repererNourriture(List<Cible> objetsReperes){
		foreach (Cible cible in objetsReperes){
			if(isNourriture(cible.getType())&& cible.getDistance()<=DeplacementsFourmisScript.DISTANCE_CASE){
				return cible;
			}
		}
		return null;
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
	
	void prendreNourriture(IAnourriture nourriture){
		nourriture.prendreNourriture ();
	}
	
	override public EntiteApointsDeVie getModele(){
		return this.modele;
	}
	
	override public TypesCamps getCamp(){
		return this.monCamp;
	}
	
	private bool isNourriture(TypesObjetsRencontres type){
		if (type == TypesObjetsRencontres.BONBON_ANGLAIS_BLEU || type == TypesObjetsRencontres.BONBON_ANGLAIS_ROSE || type == TypesObjetsRencontres.BONBON_MURE || type == TypesObjetsRencontres.BONBON_ORANGE || type == TypesObjetsRencontres.BONBON_ROSE || type == TypesObjetsRencontres.BONBON_VERT) {
			return true;
		}
		return false;
	}
	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}
	
	protected Cible repererReine(List<Cible> objetsReperes){
		foreach (Cible cible in objetsReperes) {
			if ( cible.getMonIAabstraite() != null ){
				if (cible.getMonIAabstraite().getCamp()==getCamp ()) {
					if(getCamp() == TypesCamps.BLANC){
						if(cible.getType() == TypesObjetsRencontres.REINE_BLANCHE){
							return cible;
						}
					}else{
						if(cible.getType() == TypesObjetsRencontres.REINE_NOIRE){
							return cible;
						}
					}
				}
			}
		}
		return null;
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


