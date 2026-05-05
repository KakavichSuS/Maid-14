# Poggers examine system

examine-name = Это же [bold]{ $name }[/bold].
examine-name-selfaware = Это вы, [bold]{ $name }[/bold].
examine-can-see = Осмотрев { OBJECT($ent) }, вы можете увидеть:
examine-can-see-nothing = { CAPITALIZE(SUBJECT($ent)) } полностью без ничего!
examine-border-line = ═════════════════════
examine-present-tex = Это [enttex id="{ $id }" size={ $size }] [bold]{ $name }[/bold].
examine-present = Это [bold]{ $name }[/bold].
examine-present-line = ═══

id-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
head-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на голове.
eyes-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на глазах.
mask-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на лице.
neck-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на шее.
ears-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ушах.
jumpsuit-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
outer-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
suitstorage-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ремне.
back-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на спине.
gloves-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на руках.
belt-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на поясе.
shoes-examine = { GENDER($ent) ->
    [male] Он носит
    [female] Она носит
    [epicene] Они носят
   *[neuter] Оно носит
} { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ногах.

id-card-examine-full = { CAPITALIZE(POSS-ADJ($wearer)) } ID: [bold]{ $nameAndJob }[/bold].

# Selfaware version

examine-can-see-selfaware = Осмотрев себя, вы можете увидеть:
examine-can-see-nothing-selfaware = На вас вообще ничего нет!

id-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
head-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на голове.
eyes-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на глазах.
mask-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на лице.
neck-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на шее.
ears-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ушах.
jumpsuit-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
outer-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
}.
suitstorage-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ремне.
back-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на спине.
gloves-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на руках.
belt-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на поясе.
shoes-examine-selfaware = Вы носите { $id ->
     [empty] [bold]{ $item }[/bold]
    *[other] [enttex id="{ $id }" size={ $size }][bold]{ $item }[/bold]
} на ногах.

# Selfaware examine

comp-hands-examine-empty-selfaware = Вы ничего не держите.
comp-hands-examine-selfaware = Вы держите { $items }.

humanoid-appearance-component-examine-selfaware = Вы — { $age } { $species }.
