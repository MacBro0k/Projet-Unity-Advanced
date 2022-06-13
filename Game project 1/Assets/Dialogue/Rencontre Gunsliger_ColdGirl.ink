-> main

== main ==
J'adore le pain
quelle cuisson ?
    + [bien cuit]
        -> chosen("genre tu gry du charbon mon reuf")
    + [normal]
        -> chosen("logique")
    + [j'aime la pate pas cuite]
        -> chosen("chelou mon reuf")
        
== chosen(Pain) ==
{Pain}
->END