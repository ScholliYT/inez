# INEZ - Der INtelligente EinkaufsZettel
[![Build Status](https://tom-ein-stein.visualstudio.com/inez/_apis/build/status/inez-app%20-%20CI?branchName=master)](https://tom-ein-stein.visualstudio.com/inez/_build/latest?definitionId=1&branchName=master)

Basierend auf der [Aufgabenstellung](https://www.it-talents.de/foerderung/code-competition/edeka-digital-code-competition-08-2019) von IT-Talents ist dies eine Implementierung des Einkaufszettels als Webanwendung.

Die Webanwendung läuft derzeit in der Azure Cloud: https://inez-app.azurewebsites.net/ (Es kann sein, dass die Website lange lädt (10 Sekunden), da Azure das hosting "pausiert" falls die Website nicht genutzt wird. Azure muss das hosting dann erst wieder starten)


#### Features
- Automatische erkennung der Menge
  - "Milch" -> "1 Liter Milch"
  - "Eier" -> "10 Eier"
  - "Schokolade" -> "100g Schokolade"
- Accountsystem
- Einkaufsliste für jeden Benutzer
- Abhaken von Einträgen auf der Einkaufsliste


## Azure

Die Webanwendung läuft derzeit auf der Infrastruktur von Microsoft Azure.

#### Verwendete Azure Dienste:
- SQL server
- SQL database (1 * User, 1 * Einkaufszettel Daten)
- App Service plan
- App Service
- SignalR

Zudem wird eine Azure DevOps Pipeline verwendet um automatisch änderungen am master branch auf die Webanwendung zu übertragen.


## Datenquellen

#### Produktdaten
Die Daten wurden am 26.08.2019 von: https://world.openfoodfacts.org/ bezogen. Gefiltert nach "stores" = "edeka" und weitergehend manuell aufbereitet. 
