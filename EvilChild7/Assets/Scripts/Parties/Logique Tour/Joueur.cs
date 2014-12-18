using UnityEngine;
using System.Collections.Generic;

public class Joueur{

	protected IAreine maReine;
	protected int nbTours;
	public List<BetisesTexturees> sacAmalice;
	protected List<Oeuf> sacOeufs;
	protected bool estAfk = false;
	protected string nameJoueur;
	protected bool isBonCote;

	public Joueur(IAreine maReine,bool isBonCote){

		this.maReine = maReine;
		this.nbTours = 0;
		this.sacAmalice = new List<BetisesTexturees> ();
		this.sacOeufs = new List<Oeuf> ();
		this.nameJoueur = "";
		this.isBonCote = isBonCote;

	}

	public void addBetise(BetisesTexturees betise){
		sacAmalice.Add(betise);
	}

	public bool removeBetise(BetisesTexturees betise){

		if(sacAmalice.Contains(betise)){
			//Debug.Log ("coucou");
			sacAmalice.Remove(betise);
			return true;
		}

		return false;
	}

	public void addOeuf(Oeuf oeuf){
		sacOeufs.Add(oeuf);
	}
	
	public bool removeOeuf(Oeuf oeuf){
		
		if(sacOeufs.Contains(oeuf)){
			sacOeufs.Remove(oeuf);
			return true;
		}
		
		return false;
	}

	public void addOeufs(List<Oeuf> listOeufs){
		sacOeufs.AddRange (listOeufs);
	}

	public int nbOeufs(){
		return this.sacOeufs.Count;
	}

	public int nbBetise(){
		return this.sacAmalice.Count;
	}

	public BetisesTexturees getBetiseAt(int index){
		if (this.sacAmalice.Count > index) {
			return this.sacAmalice [index];
		}
		return null;
	}

	public Oeuf getOeufAt(int index){
		if (this.sacOeufs.Count > index) {
			return this.sacOeufs [index];
		}
		return null;
	}

	public int getTour(){
		return this.nbTours;
	}

	public void augmenterTour(){
		this.nbTours = this.nbTours + 1;
	}

	public IAreine getIaReine(){
		return this.maReine;
	}

	public  bool absent(){
		return this.estAfk;
	}

	public  bool setAfk(bool afk){
		this.estAfk = afk;
		return this.estAfk;
	}

	public  void setNameJoueur(string nom){
		this.nameJoueur = nom;
	}

	public  string getNameJoueur(){
		return this.nameJoueur;
	}

	public bool getBonCote(){
		return isBonCote;
	}

	
}