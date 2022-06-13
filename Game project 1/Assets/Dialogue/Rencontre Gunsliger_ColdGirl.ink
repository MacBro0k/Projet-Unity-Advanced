-> main

== main ==
Salut, tu n'as pas l'air de venir d'ici ?
qu'est-ce qui t'amène ?
    + [Où sommes nous ?]
        -> chosen("Nous sommes sur la planète Keploid")
    + [Comment survivre ici ?]
        -> chosen("Tu vas rencontrer bon nombre d'ennemies," 
		"alors j'espère que tu es bien armé")
        
== chosen(Pain) ==
{Pain}
->END