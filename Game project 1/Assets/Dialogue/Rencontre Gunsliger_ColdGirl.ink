-> main

== main ==
J'adore le pain
Comment est ta Bite ?
    + [Enorme]
        -> chosen("AH oui elle est vraiment grosse")
    + [Normal]
        -> chosen("Ptit ego grosse bite")
    + [Très Petite]
        -> chosen("Ah c'est triste mais au moins t'es honnête")
        
== chosen(Bite) ==
{Bite}
->END