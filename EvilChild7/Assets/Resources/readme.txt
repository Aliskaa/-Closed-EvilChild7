Un GameObject pour le terrain, ici "Bac à sable" doit avoir plusieurs scripts
et composants liés à lui afin de gérer les collisions, la géénraiton du terrain, etc.

Liste des composants :
	- Ajout d'un rigid body
			- Pour les collisions
			- Freeze position et rotation pour X, Y, Z
			- Use kinematic, pas de use gravity
	- Ajout d'un Box Collider pour chaque coté
			- Center: X=0 Y=0 Z=0
			- Size: X=1 Y=2 Z=1
	- Transform du GO :
			- position : 47, -0.1, 43
			- rotation : 0, 0, 0
			- scale : 1, 1, 1
	- Transform du fond :
			- position : 4.605492, 0, 43
			- rotation : 0, 0, 0
			- scale : 11, 1, 11
	- Transform de coté 1:
			- position : 4.29, 0, -46
			- rotation : 0, 0, 0
			- scale : 104, 2, 2
	- Transform de coté 2:
			- position : 4.29, 0, -45.7
			- rotation : 0, 0, 0
			- scale : 104, 2, 2
	- Transform de coté 3:
			- position : -48, 0, 0
			- rotation : 0, 90, 0
			- scale : 93.6, 2, 2
	- Transform de coté 4:
			- position : 55.5, 0, 1
			- rotation : 0, 90, 0
			- scale : 92, 2, 2			
			
Liste des scripts :
	- TerrainManagerScript
			- Pour générer le terrain
			- Hex Radius Size = 3
			- Texture terrain sable: sable_1
			- Texture terrain eau = eau_1
			- Taille Map X=20, Y=20
			- Taille Piece = 4
			- Parent terrain : Bac à sable
			- Position en X=-49.25
			- Position en Y=0.1
			- Position en Z=-46.5
			
Liste des tags :
	- Game Object "Bac à sable" : tag BAC_A_SABLE
	- Game Object "Selection case" : tag POINTEUR
