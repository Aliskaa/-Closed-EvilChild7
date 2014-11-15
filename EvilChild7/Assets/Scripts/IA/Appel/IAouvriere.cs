using UnityEngine;
using System.Collections.Generic;

public class IAouvriere: IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Ouvriere modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	protected TypesAxes derniereDirection = TypesAxes.AUCUN;
	protected GameObject dernierePheromone = null;

	public List<GameObject> dernieresPheromonesVUes = new List<GameObject> ();

	public IAouvriere(TypesObjetsRencontres monType, IAreaction reaction){
		attaquants = new List<IAabstraite> ();
		victimes = new List<IAabstraite> ();
		modele = new Ouvriere();
		if (monType == TypesObjetsRencontres.OUVRIERE_BLANCHE) {
			monCamp = TypesCamps.BLANC;
			this.monType = TypesObjetsRencontres.OUVRIERE_BLANCHE;
		} else {
			monCamp = TypesCamps.NOIR;
			this.monType = TypesObjetsRencontres.OUVRIERE_NOIRE;
		}
		
		maReaction = reaction;

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
					maReaction.poserPheromones(false);
					rentrerBase();
					
				}
				
				
			}else{
				
				Cible objetNourriture = repererNourriture(objetsReperes);
				
				if(objetNourriture != null){
					
					if(objetNourriture.getDistance()<=DeplacementsFourmisScript.DISTANCE_CASE){
						derniereDirection = TypesAxes.AUCUN;
						dernierePheromone = null;
						modele.nourriturePrise();
						prendreNourriture((IAnourriture)objetNourriture.getMonIAobjet());
						maReaction.poserPheromones(true);
						
					}else{
						
						bouger (objetNourriture.getDirection());
						
					}
				}else{
					Cible objetPheromone = repererPheromones(objetsReperes);
					if(objetPheromone != null){
						derniereDirection = objetPheromone.getDirection();
						dernierePheromone = objetPheromone.getGameObject();
						dernieresPheromonesVUes.Add(dernierePheromone);
						bouger(objetPheromone.getDirection());
					}else{
						dernieresPheromonesVUes.Clear();
						dernierePheromone = null;
						deambuler();
					}
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
		if (type >= TypesObjetsRencontres.BONBON_ANGLAIS_BLEU && type <=TypesObjetsRencontres.BONBON_VERT) {
			return true;
		}
		return false;
	}
	
	private Cible repererPheromones(List<Cible> objetsReperes){
		Cible pheromoneOuv = null;
		Cible pheromoneCm = null;

		List<Cible> ciblesGOok = new List<Cible> ();

		bool flagDejaVu = false;
		foreach (Cible cible in objetsReperes) {
			if(cible.getMonIAobjet() == null){
				GameObject goCourant = cible.getGameObject();
				foreach (GameObject go in dernieresPheromonesVUes ){
					if ( go == goCourant ){
						flagDejaVu = true;
					}
				}
				if (!flagDejaVu) {
					Debug.Log("Ajouté nouvelle cible :"+ciblesGOok.Count);
					ciblesGOok.Add(cible); 
				}
			}
			flagDejaVu = false;
		}

		Debug.Log("Nombre de cibles : "+ciblesGOok.Count);
		//foreach (Cible cible in objetsReperes) {
		foreach (Cible cible in ciblesGOok) {	


			if(cible.getMonIAobjet() == null){

				/*
				// Le cas où c'est la dernière trouvé
				if ( cible.getGameObject() == dernierePheromone){
					Debug.Log("Dernière : merde");
					continue;
				}
*/

				//if(!tourneEnRond(cible)){

					if(this.getCamp() == TypesCamps.BLANC){
						
						if(cible.getType () == TypesObjetsRencontres.PHEROMONES_CM_BLANCHE){
							
							if(pheromoneCm ==null){
								
								pheromoneCm = cible;
								
							}else{
								
								if(pheromoneCm.getDistance() > cible.getDistance()){
									
									pheromoneCm = cible;
									
								}
							}
						}
						
						if(cible.getType () == TypesObjetsRencontres.PHEROMONES_OUV_BLANCHE){
							
							if(pheromoneOuv ==null){
								
								pheromoneOuv = cible;
								
							}else{
								
								if(pheromoneOuv.getDistance() > cible.getDistance()){
									
									pheromoneOuv = cible;
								}
							}
						}
					}else{
						
						if(cible.getType () == TypesObjetsRencontres.PHEROMONES_CM_NOIRE){
							
							if(pheromoneCm ==null){
								
								pheromoneCm = cible;
								
							}else{
								
								if(pheromoneCm.getDistance() > cible.getDistance()){
									
									pheromoneCm = cible;
									
								}
							}
						}
						
						if(cible.getType () == TypesObjetsRencontres.PHEROMONES_OUV_NOIRE){
							
							if(pheromoneOuv ==null){
								
								pheromoneOuv = cible;
								
							}else{
								
								if(pheromoneOuv.getDistance() > cible.getDistance()){
									
									pheromoneOuv = cible;
								}
							}
						}
					}
				//}
			}
		}
		
		if (pheromoneCm != null) {
			return pheromoneCm;
		}
		
		if (pheromoneOuv != null) {
			return pheromoneOuv;
		}
		
		return null;
	}
	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}

	/*
	protected bool tourneEnRond(TypesAxes direction){

		
		if (derniereDirection == TypesAxes.AUCUN) {
			return false;
		}
		
		//TypesAxes direction = pheromone.getDirection ();
		if(this.derniereDirection == TypesAxes.DEVANT){
			if(direction == TypesAxes.DERRIERE){
				return true;
			}
			
		}
		
		if(this.derniereDirection == TypesAxes.DERRIERE){
			if(direction == TypesAxes.DEVANT){
				return true;
			}
		}
		
		if(this.derniereDirection == TypesAxes.DEVANT_GAUCHE){
			if(direction == TypesAxes.DERRIERE_DROITE){
				return true;
			}
		}
		
		if(this.derniereDirection == TypesAxes.DEVANT_DROITE){
			if(direction == TypesAxes.DERRIERE_GAUCHE){
				return true;
			}
			return false;
		}
		
		if(this.derniereDirection == TypesAxes.DERRIERE_GAUCHE){
			if(direction == TypesAxes.DEVANT_DROITE){
				return true;
			}
		}
		
		if(this.derniereDirection == TypesAxes.DERRIERE_DROITE){
			if(direction == TypesAxes.DEVANT_GAUCHE){
				return true;
			}
		}
		return false;

	}
	*/

//	protected bool tourneEnRond(GameObject pheromone){

		//if (pheromone == null)return false;

		/*
		if (dernierePheromone == pheromone.getMonIAobjet()) {
			Debug.Log("pheros pareil tourne en rond patapon");
			return true;
		}
		return false;
*


		if(dernierePheromone !=null){
			if ( pheromone.getDistance ()<= DeplacementsFourmisScript.DISTANCE_CASE/2){
				Debug.Log("Trop proche : merde");
				return true;
			}else{
				if(dernierePheromone == pheromone.getGameObject()){
					return true;
				}
			}
		}
		return false;	
*/


//	}


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