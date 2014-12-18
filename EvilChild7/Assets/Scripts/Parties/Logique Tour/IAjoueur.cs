using UnityEngine;
using System.Collections.Generic;

public class IAjoueur:Joueur
{
	private TourDeJeu tour;
	private Interface monInterface;
	private GameObject bacAsable;
	public bool tourFini;
	public bool actionLancees;

	public IAjoueur(IAreine reine,TourDeJeu tour,Interface monInterface, bool isBonCote,GameObject bacAsable):base(reine,isBonCote)
	{
		this.tour = tour;
		this.bacAsable = bacAsable;
		tourFini = false;
		actionLancees = false;
	}

	public void deroulerTour(){
		if (!actionLancees) {
			lancerOeufs ();
			lancerBetises();
			this.tourFini = true;
			}
	}


	public void lancerOeufs(){
		int oeufsAlancer = sacOeufs.Count / 2;
		for (int i = 0; i < oeufsAlancer; i++) {
				if(tour.getPTACutilise()+ (int)PoidsObjetsAlancer.POIDS_OEUFS  <tour.getPTAC()){
					Oeuf tmp = getOeufAt (0);
					removeOeuf (tmp);

					if(isBonCote){
						Betises  tmpBet = bacAsable.AddComponent<Betises>();
						tmpBet.nom_betise = Invocations.OEUF_FOURMI;
						tmpBet.maFourmi = tmp.getType();
						tmpBet.lancerIA = true;
					}else{
						Betises2 tmpBet = bacAsable.AddComponent<Betises2>();
						tmpBet.nom_betise = Invocations.OEUF_FOURMI;
						tmpBet.maFourmi = tmp.getType();
						tmpBet.lancerIA = true;
					}
					tour.augmenterPTACutilise((int)PoidsObjetsAlancer.POIDS_OEUFS);
				//Debug.Log ("Un Oeuf !");
				}else{
					i = oeufsAlancer+1;
				}
			}

	}
	public void selectionnerUneBetise(List<BetisesTexturees> mesBetises){
		int tp = Random.Range (0, mesBetises.Count-1);
		sacAmalice.Add (mesBetises [tp]);
	}

	public void lancerBetises(){
		int index = containsNourriture ();
		if (index != -1) {
			NourritureBonbons n = new NourritureBonbons ();
			Invocations nom_bonbon = n.ListeNourriture();
			if(isBonCote){
			
				Betises  tmp = bacAsable.AddComponent<Betises>();
				tmp.nom_betise = nom_bonbon;
				tmp.lancerIA = true;
			}else{
				
				Betises2  tmp = bacAsable.AddComponent<Betises2>();
				tmp.nom_betise = nom_bonbon;
				tmp.lancerIA = true;
			}
		}
		bool notFinish = true;
		while (notFinish) {
			if(sacAmalice.Count <=0){
				notFinish = false;
			}else{
				int indexTmp= Random.Range (0, sacAmalice.Count - 1);
				int poids = (int)PoidsObjetsAlancerM.indexToPoids(sacAmalice [indexTmp].getBetise ());
				if(tour.getPTACutilise() + poids  <tour.getPTAC()){
				lancerUneBetise (sacAmalice [indexTmp].getBetise ());
					sacAmalice.RemoveAt(indexTmp);
					tour.augmenterPTACutilise(poids);
				}else{
					notFinish = false;
				}
			}
		}
	}

	public void faireUnLancer(){
		int index = Random.Range (0, sacAmalice.Count - 1);
		lancerUneBetise (sacAmalice [index].getBetise ());
	}

	public void setTour(TourDeJeu tour){
		this.tour = tour;
	}

	public int canUseDeuxFois(){
		int tmp = 0;
		for (int i = 0; i < sacAmalice.Count; i++) {
			BetisesIndex maBetise = sacAmalice[i].getBetise();
			if( maBetise != BetisesIndex.GELEE ||maBetise != BetisesIndex.DOUBLE_LANCER){
					tmp ++;
				}
		}
		return tmp;
	}

	public int canUseLancerGros(){
		int tmp = 0;
		for (int i = 0; i < sacAmalice.Count; i++) {
			BetisesIndex maBetise = sacAmalice[i].getBetise();
			if( utilisePoids(maBetise)){
				tmp ++;
			}
		}
		return tmp;
	}

	public int canUseVentilateur(){
		int tmp = 0;
		for (int i = 0; i < sacAmalice.Count; i++) {
			BetisesIndex maBetise = sacAmalice[i].getBetise();
			if(utiliseVentilo(maBetise)){
				tmp ++;
			}
		}
		return tmp;
	}


	public int containsNourriture(){
		int tmp = 0;
		for (int i = 0; i < sacAmalice.Count; i++) {
			BetisesIndex maBetise = sacAmalice[i].getBetise();
			if( maBetise == BetisesIndex.BONBON){
				return i;
			}
		}
		return -1;
	}

	public bool aFini(){
		return this.tourFini;
	}

	public bool utiliseVentilo(BetisesIndex maBetise){
		if (maBetise == BetisesIndex.GELEE) {
			return false;
		}

		if (maBetise == BetisesIndex.DOUBLE_LANCER) {
			return false;
		}

		if (maBetise == BetisesIndex.PISTOLET) {
			return false;
		}

		if (maBetise == BetisesIndex.GELEE) {
			return false;
		}

		if (maBetise == BetisesIndex.LANCER_GROS) {
			return false;
		}

		if (maBetise == BetisesIndex.VENTILATEUR) {
			return false;
		}
		return true;
	}
	public bool utilisePoids(BetisesIndex maBetise){
		if (maBetise == BetisesIndex.GELEE) {
				return false;
		}

		if (maBetise == BetisesIndex.DOUBLE_LANCER) {
				return false;
		}

		if (maBetise == BetisesIndex.PISTOLET) {
				return false;
		}

		if (maBetise == BetisesIndex.GELEE) {
				return false;
		}

		if (maBetise == BetisesIndex.LANCER_GROS) {
				return false;
		}

		if (maBetise == BetisesIndex.VENTILATEUR) {
				return false;
		}
		return true;
	}
	protected void lancerUneBetise(BetisesIndex index){
		Betise maBetise;
		switch (index) {
			
		case BetisesIndex.CAILLOU:
			//Debug.Log("caillou selectionné");
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.CAILLOU;
				((Betises)maBetise).lancerIA = true;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.CAILLOU;
				((Betises2)maBetise).lancerIA = true;
				
			}
			
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.PISTOLET:
			//Debug.Log("pistolet selectionné");
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.MISSILE_EAU;
				((Betises)maBetise).lancerIA = true;

			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.MISSILE_EAU;
				((Betises2)maBetise).lancerIA = true;

			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.BONBON:
			//Debug.Log("bonbons selectionné");
			NourritureBonbons n = new NourritureBonbons ();
			Invocations nom_bonbon = n.ListeNourriture();
			
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = nom_bonbon;
				((Betises)maBetise).lancerIA = true;

			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = nom_bonbon;
				((Betises2)maBetise).lancerIA = true;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.BOUT_DE_BOIS:
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.BOUT_DE_BOIS;
				((Betises)maBetise).lancerIA = true;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.BOUT_DE_BOIS;
				((Betises2)maBetise).lancerIA = true;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.DOUBLE_LANCER:
			//Debug.Log("deux fois selectionné");
			LancerDeuxFois tmpDeuxFois = new LancerDeuxFois();
			tmpDeuxFois.lancerCeTour(tour);
			break;
			
		case BetisesIndex.LANCER_GROS:
			//Debug.Log("force selectionné");
			LancerGros tmpGros = new LancerGros();
			tmpGros.lancerCeTour(tour);
			break;
			
		case BetisesIndex.VENTILATEUR:
			//Debug.Log("ventilateur selectionné");
			Ventilateur monVentilateur = new Ventilateur();
			monVentilateur.lancerCeTour(tour);
			break;
			
		case BetisesIndex.BOMBE_EAU:
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.BOMBE_EAU;
				((Betises)maBetise).lancerIA = true;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.BOMBE_EAU;
				((Betises2)maBetise).lancerIA = true;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.SCARABEE:
			if(this.getBonCote()){
				maBetise = bacAsable.AddComponent<Betises> ();
				((Betises)maBetise).nom_betise = Invocations.SCARABEE;
				((Betises)maBetise).lancerIA = true;
			}else{
				maBetise = bacAsable.AddComponent<Betises2> ();
				((Betises2)maBetise).nom_betise = Invocations.SCARABEE;
				((Betises2)maBetise).lancerIA = true;
			}
			tour.augmenterPTACutilise(maBetise.getPoids());
			break;
			
		case BetisesIndex.GELEE:
			//Debug.Log("Gelée royale selectionné");
			int tmp = Random.Range(0,1);
			Dopage monDopage =  new Dopage(this.getIaReine(),(TypeDopage)tmp);
			monDopage.lancer();
			break;
			
		default:
			break;    
		}
		
	}
}

