
public class IAreineBlanche:IAreine
{
	public IAreineBlanche (IAreaction maReaction):base(maReaction)
		{
			this.monType = TypesObjetsRencontres.REINE_BLANCHE;
		}

	public override Oeuf genererOeuf(){
		float limiteOuvriere = modele.getRatioOuvriere ();
		System.Random random = new System.Random ();
		float resultat = (float)random.NextDouble();
		
		if (resultat <= limiteOuvriere) {
			resultat = (float)random.NextDouble();

			if(resultat < modele.getSpeciale()){
				return new Oeuf(TypesObjetsRencontres.CONTREMAITRE_BLANCHE);
			}

			return new Oeuf(TypesObjetsRencontres.OUVRIERE_BLANCHE);
		}

		resultat = (float)random.NextDouble();
		if (resultat < modele.getSpeciale ()) {
			return new Oeuf (TypesObjetsRencontres.GENERALE_BLANCHE);
		}

		return new Oeuf(TypesObjetsRencontres.COMBATTANTE_BLANCHE);
	}
}


