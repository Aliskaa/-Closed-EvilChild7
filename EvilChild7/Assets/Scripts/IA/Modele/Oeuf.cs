
public class Oeuf
{
	
	private static  int MAX_TOURS = 5;
	private static int MIN_TOURS = 2;
	private static int PTAC_OEUF = 2;
	
	private int tours;
	private TypesObjetsRencontres monType;
	private int ptac = PTAC_OEUF;
	
	public Oeuf (TypesObjetsRencontres monType)
	{
		System.Random random = new System.Random();
		this.monType = monType;
		this.tours = random.Next (MIN_TOURS, MAX_TOURS);
	}
	
	public int getPTAC(){
		return ptac;
	}
	
	public int getTours(){
		return this.tours;
	}
	
	public void enleverTour(){
		this.tours = this.tours - 1;
		if (this.tours <= 0) {
			eclore ();
		}
	}
	
	public TypesObjetsRencontres getType(){
		return monType;
	}
	
	public void eclore(){
	}
}


