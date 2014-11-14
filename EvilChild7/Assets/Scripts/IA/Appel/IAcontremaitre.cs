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
	}
	override public void signaler(IAreaction moi, List<Cible> objetsReperes){
		
		if (maReaction == null) {
			maReaction = moi;
		}
		if(attaque()) {
			
			attaquer(getAttaquantAt(0));
			
		}else{
			
			if (modele.transporteNourriture()) {
				
				rentrerBase();
				
			}else{
				
				Cible objetNourriture = repererNourriture(objetsReperes);
				
				if(objetNourriture != null){
					
					if(objetNourriture.getDistance()<=1){
						
						modele.nourriturePrise();
						prendreNourriture((Nourriture)objetNourriture.getObjet());
						
					}else{
						
						bouger (objetNourriture.getDirection());
						
					}
				}else{
					maReaction.poserPheromones();
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
	
	override public void attaquer(IAabstraite ennemy){
		ennemy.ajouterAttaquant (this);
		if (!victimes.Contains (ennemy)) {
			victimes.Add (ennemy);
		}
		int PVRestants = ennemy.getModele ().enleverPV (modele.getAttaque ());
		if (PVRestants <= 0) {
			ennemy.mort ();
		}
		
	}
	
	private Cible repererNourriture(List<Cible> objetsReperes){
		foreach (Cible cible in objetsReperes){
			if(isNourriture(cible.getType())&& cible.getDistance()<=1){
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
	
	void prendreNourriture(Nourriture nourriture){
		nourriture.diminuerQuantite ();
	}
	
	override public Modele getModele(){
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
	
}

