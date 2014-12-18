public class Ventilateur:Betise
{

	private TourDeJeu monTour;
	
	public void lancer(){
		this.monTour.getVent ().reroll ();
	}
	
	public int getPoids(){
		return 0;
	}

	public void lancerCeTour(TourDeJeu tour){
		this.monTour = tour;
		lancer ();
	}
}


