inspect-part-status-line = {
  $isSelf ->
    [true] {
      $partType ->
        [Groin] Ваш
        [Tail] Ваш
       *[other] Ваша
      }
    *[false] { CAPITALIZE(POSS-ADJ($entity)) }
} { $part } { $status }.
inspect-part-status-title = Вы осматриваете себя на предмет повреждений.
inspect-part-status-title-other = Вы осматриваете { POSS-ADJ($entity) } на предмет повреждений.
inspect-part-status-fine = в порядке
inspect-part-status-comma = ,{ " " }
inspect-part-status-and = и{ " " }

inspect-part-status-Head = голова
inspect-part-status-Chest = грудь
inspect-part-status-Groin = пах
inspect-part-status-Arm = рука
inspect-part-status-LeftArm = левая рука
inspect-part-status-RightArm = правая рука
inspect-part-status-Hand = кисть
inspect-part-status-LeftHand = левая кисть
inspect-part-status-RightHand = правая кисть
inspect-part-status-Leg = нога
inspect-part-status-LeftLeg = левая нога
inspect-part-status-RightLeg = правая нога
inspect-part-status-Foot = стопа
inspect-part-status-LeftFoot = левая стопа
inspect-part-status-RightFoot = правая стопа
inspect-part-status-Tail = хвост
inspect-part-status-Other = часть тела
