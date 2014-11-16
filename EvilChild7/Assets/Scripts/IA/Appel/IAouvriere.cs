using UnityEngine;
using System.Collections.Generic;

public class IAouvriere: IAabstraite
{
	
	protected IAreaction maReaction = null;
	protected Ouvriere modele;
	protected TypesCamps monCamp;
	protected TypesObjetsRencontres monType;
	protected TypesAxes derniereDirection = TypesAxes.AUCUN;
	protected TypesAxes[] axes = new TypesAxes[6]{TypesAxes.DEVANT,TypesAxes.DEVANT_DROITE,TypesAxes.DERRIERE_DROITE,TypesAxes.DERRIERE,TypesAxes.DERRIERE_GAUCHE,TypesAxes.DEVANT_GAUCHE};
	
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
					//maReaction.poserPheromones(false);
					rentrerBase();
					
				}
				
				
			}else{
				
				Cible objetNourriture = repererNourriture(objetsReperes);
				
				if(objetNourriture != null){
					
					if(objetNourriture.getDistance()<=DeplacementsFourmisScript.DISTANCE_CASE){
						//derniereDirection = TypesAxes.AUCUN;
						modele.nourriturePrise();
						prendreNourriture((IAnourriture)objetNourriture.getMonIAobjet());
						maReaction.poserPheromones(true);
						
					}else{
						
						bouger (objetNourriture.getDirection());
						
					}
				}else{
					Cible objetPheromone = repererPheromones(objetsReperes);
					if(objetPheromone != null){
						//Debug.Log("Je vais en "+objetPheromone.getDirection());
						bouger(objetPheromone.getDirection());
					}else{
						deambuler();
					}
				}
			}
		}
		
	}
	
	void bouger(TypesAxes direction){
		maReaction.bouger(direction,modele.getMouvement());
		derniereDirection = direction;
	}
	
	void deambuler(){
		TypesAxes dir = maReaction.deambuler ();
		derniereDirection = dir;
	}
	
	void rentrerBase(){
		TypesAxes dir = maReaction.rentrerBase ();
		derniereDirection = dir;
	}
	
	public void attaquer(IAabstraite ennemy){
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

		/*
		 * Etape 0 : Séparer les phéromones par couleurs
		 */
		List<Cible> pheromonesNoires = new List<Cible> ();
		List<Cible> pheromonesBlanches = new List<Cible> ();

		foreach (Cible cible in objetsReperes) {
			if (cible.getMonIAobjet () == null) {
				TypesObjetsRencontres tor = cible.getType ();
				if (tor == TypesObjetsRencontres.PHEROMONES_CM_BLANCHE || tor == TypesObjetsRencontres.PHEROMONES_OUV_BLANCHE) {
						pheromonesBlanches.Add (cible);
				} else {
						pheromonesNoires.Add (cible);
				}
			}
		}

		/*
		 * Etape 1 : Séparer les phéromones d'ouvrières des phéormones de contremaitre
		 */
		List<Cible> pheromonesNoiresCm = new List<Cible>();
		List<Cible> pheromonesNoiresOuv = new List<Cible>();
		List<Cible> pheromonesBlanchesCm = new List<Cible>();
		List<Cible> pheromonesBlanchesOuv = new List<Cible>();

		foreach ( Cible c in pheromonesNoires ){
			TypesObjetsRencontres tor = c.getType();
			if (tor == TypesObjetsRencontres.PHEROMONES_CM_NOIRE) {
				pheromonesNoiresCm.Add(c);
			} else {
				pheromonesNoiresOuv.Add(c);
			}
		}
		pheromonesNoires.Clear();
		pheromonesNoires = null;

		foreach (Cible c in pheromonesBlanches) {
			TypesObjetsRencontres tor = c.getType ();
			if (tor == TypesObjetsRencontres.PHEROMONES_CM_BLANCHE) {
				pheromonesBlanchesCm.Add (c);
			} else {
				pheromonesBlanchesOuv.Add (c);
			}
		}
		pheromonesBlanches.Clear();
		pheromonesBlanches = null;

		Cible pheroPlusProche = null;

		/*
		* Etape 2 : Traiter d'abord les phéromones de contremaitre : on choisir une à la fin
		* 			Prendre la phéromone qui :
		* 				- est la plus proche
		* 				- n'a pas déjà été prise
		* 					- donc pour ce cas, on ne change pas de sens
		*/
		if (this.getCamp () == TypesCamps.BLANC) {
			pheroPlusProche = chercherPlusProche(pheromonesBlanchesCm);
		} else {
			pheroPlusProche = chercherPlusProche(pheromonesNoiresCm);
		}

		if (pheroPlusProche != null) return pheroPlusProche;

		/*
		 * Etape 3 : Si pas de phéromone de contremaitre trouvées, on traite les phéromones des ouvrières
		 */
		if (this.getCamp () == TypesCamps.BLANC) {
			pheroPlusProche = chercherPlusProche(pheromonesBlanchesOuv);
		} else {
			pheroPlusProche = chercherPlusProche(pheromonesNoiresOuv);
		}

		/*
		 * Etape 3.2 : Suivre la direction de la phéromone.
		 * Attention, il faut prendre l'inverse de la direction de cette phéromone pour les ouvrières (bouffe dans l'autre sens)
		 */
		// TODO

		/*
		 * Etape 3.3 : Cas où plusieurs directions
		 */
		// TODO

		/*
		 * Etape 4 : Aller vers la phéromone désirée
		 */
		return pheroPlusProche;

	}

	private Cible chercherPlusProche( List<Cible> liste ){

		if (liste == null || liste.Count <= 0)	return null;
		Cible pheroPlusProche = null;

		// S'il n'y a que des phéromones derrière
		if (rienDevant (liste)) {
			foreach (Cible c in liste) {
				if ((pheroPlusProche == null || c.getDistance () < pheroPlusProche.getDistance ())) {
					pheroPlusProche = c;
				}
			}
		// Il y a des phéromones devants
		} else {
			foreach (Cible c in liste) {
				// Il y a des phéromones devant
				if (! demiTour (c)) {
					if ((pheroPlusProche == null || c.getDistance () < pheroPlusProche.getDistance ())) {
						pheroPlusProche = c;
					}
				}
			}
		}

		return pheroPlusProche;

	}

	private bool rienDevant(List<Cible> liste){
		foreach (Cible c in liste) {
			if ( c.getDirection() == TypesAxes.DEVANT ) return false;
			if ( c.getDirection() == TypesAxes.DEVANT_DROITE ) return false;
			if ( c.getDirection() == TypesAxes.DEVANT_DROITE ) return false;
		}
		return true;
	}

	private bool demiTour( Cible c ){
		TypesAxes directionReperageCible = c.getDirection();
		return ( directionReperageCible == TypesAxes.DERRIERE || directionReperageCible == TypesAxes.DERRIERE_DROITE
		        || directionReperageCible == TypesAxes.DERRIERE_GAUCHE);
	}	


	
	override public TypesObjetsRencontres retourType(){
		return this.monType;
	}
	
	protected TypesAxes oppose(TypesAxes direction){
		
		if(direction == TypesAxes.DEVANT){
			return TypesAxes.DERRIERE;
		}

		if(direction == TypesAxes.DERRIERE){
			return TypesAxes.DEVANT;
		}

		if(direction == TypesAxes.DEVANT_GAUCHE){
			return TypesAxes.DERRIERE_DROITE;
		}
		
		if(direction == TypesAxes.DERRIERE_DROITE){
			return TypesAxes.DEVANT_GAUCHE;
		}

		if(direction == TypesAxes.DEVANT_DROITE){
			return TypesAxes.DERRIERE_GAUCHE;
		}
		
		if(direction == TypesAxes.DERRIERE_GAUCHE){
			return TypesAxes.DEVANT_DROITE;
		}
		return TypesAxes.AUCUN;
	}
	/*
	protected bool tourneEnRond(Cible pheromone){
		if(derniereDirection == pheromone.getDirection()){
				return true;
		}
		return false;
	}*/
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
	
	protected void ajusterRepere(TypesAxes direction){
		int pas = (int)direction - (int)TypesAxes.DEVANT;
		if (pas > 0) {
			TypesAxes[] tmp = axes;
			for(int i = 0; i<tmp.Length; i++){
				int index = pas + i;
				if(index >= 6){
					index = pas + i - 6;
					
				}
				tmp[index] = axes[i];
			}
			axes = tmp;
		}
		
	}
}