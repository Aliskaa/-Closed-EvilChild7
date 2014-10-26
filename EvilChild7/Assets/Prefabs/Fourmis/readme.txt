Un GameObject pour les fourmis, ici "Fourmis Noire" doit avoir plusieurs scripts
et composants liés à lui afin de gérer ses déplacement, son intelligence ou sa
physique par exemple.

Liste des composants :
	- Character Controller [PLUS UTILISE POUR L'INSTANT]
			- Pour faire déplacer l'objet.
			- Center: X=0 Y=4 Z=0
	- Ajout d'un rigid body
			- Pour les collisions
			- Contraints: Freeze Position en Y, freeze rotations en X, Y, Z
	- Ajout d'un Box Collider
			- Center: X=0.5 Y=2 Z=1
			- Size: X=6 Y=2.5 Z=6
			- IsTrigger à true


Liste des scripts :
	- DeplacementsFourmisScript
			- Pour gérer le déplacement de la fourmis sur le terrain ainsi que la vision
			qu'elle a de celui-ci (sa place, sa posiiton, etc.)
			- Déplacement Vitesse: 5
	- CollisionsFourmisScript
			- Pour gérer les collisions
			- Longueur visee: 5
			

