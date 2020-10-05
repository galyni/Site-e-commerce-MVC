# Site Web e-commerce MVC ASP.NET Core 3.1

Projet de fin de formation (Global Knowledge, C# ASP.NET, mai-août 2019).

#### Base de données
- Design de la base : schéma, modélisation des cardinalités des relations, établissement des clés primaires et des clés étrangères.
- Création de la base : Microsoft SQL Server Management Studio.
- Génération des classes et lien avec la base de donnée : Entity Framework (scaffolding).
- Modifications ultérieures de la base à l'aide de migrations (approche Code First).

#### Accès aux données
- Repositories, avec injection de dépendance au sein des controleurs.
- Repository générique et interface correspondante, pour une plus grande maintenabilité.
- Utilisation des propriétés de navigation et du Lazy Loading.
- Requêtes Linq to entities.

#### Controleurs
- Logique de session (panier) avec sérialisation et désérialisation Json.
- Gestion du stockage et de la récupération des images.
- Usage du ViewBag/ViewData pour le passage de données aux vues.

#### Vues
- Boutons, fenêtres modales, affichages d'images, navbars et menus déroulants.
- Formulaires avec validation des données (au sein des vues et à l'aide de data annotations), masquage des mots de passe.
- Layout commun à toutes les pages.
- Usage de vues partielles et de View Components.
- Tag Helpers et Html Helpers.
- Personnalisation du routage (dans Startup.cs et au sein des controleurs).


#### Identités et autorisations
- Asp.Net Core Identity, implémenté au sein de la même base de données.
- Gestion des accès, différents pour les utilisateurs connectés, anonymes, les administrateurs et les modérateurs.
- Logique conditionnelle interne aux vues en syntaxe Razor (par exemple, masquage de boutons) + usage d'annotations au sein des controleurs.
- Pas de stockage du mot de passe, mais du hashage.
