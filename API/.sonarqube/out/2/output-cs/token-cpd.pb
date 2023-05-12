∞[
HC:\Users\VishnuPadiga\Desktop\EventManager\Repository\EventRepository.cs
	namespace 	

Repository
 
{ 
public 
class	 
EventRepository 
:  
IEventRepository! 1
{ 
private		 
readonly		 
ApiDbContext		 !
_context		" *
;		* +
private

 
static

 
readonly

 
Logger

 "
_logger

# *
=

+ ,

LogManager

- 7
.

7 8!
GetCurrentClassLogger

8 M
(

M N
)

N O
;

O P
public 

EventRepository 
( 
ApiDbContext '
context( /
)/ 0
{ 
_context 
= 
context 
; 
} 
public 

bool 
CreateEvent 
( 

EventModel &

eventModel' 1
)1 2
{ 
_logger 
. 
Info 
( 
$str B
,B C

eventModelD N
)N O
;O P
_context 
. 
Events 
. 
Add 
( 

eventModel $
)$ %
;% &
_context 
. 
SaveChanges 
( 
) 
; 
_logger 
. 
Info 
( 
$str Q
)Q R
;R S
return 
true 
; 
} 
public&& 

List&& 
<&& 

EventModel&& 
>&& 
GetAllEvents&& (
(&&( )
int&&) ,

startIndex&&- 7
,&&7 8
int&&9 <
rowSize&&= D
)&&D E
{'' 
_logger(( 
.(( 
Info(( 
((( 
$str(( >
)((> ?
;((? @
List)) 

<))
 

EventModel)) 
>)) 
events)) 
=)) 
_context))  (
.))( )
Events))) /
.))/ 0
Where))0 5
())5 6
x))6 7
=>))8 :
x)); <
.))< =
IsActive))= E
==))F H
true))I M
)))M N
.))N O
Skip))O S
())S T

startIndex))T ^
)))^ _
.))_ `
Take))` d
())d e
rowSize))e l
)))l m
.))m n
ToList))n t
())t u
)))u v
;))v w
_logger** 
.** 
Info** 
(** 
$str** <
,**< =
events**> D
)**D E
;**E F
return++ 
events++ 
.++ 
ToList++ 
(++ 
)++ 
;++ 
},, 
public55 

List55 
<55 

EventModel55 
>55 
GetPastEvents55 )
(55) *
int55* -

startIndex55. 8
,558 9
int55: =
rowSize55> E
)55E F
{66 
_logger77 
.77 
Info77 
(77 
$str77 D
)77D E
;77E F
List88 

<88
 

EventModel88 
>88 
events88 
=88 
_context88  (
.88( )
Events88) /
.88/ 0
Where880 5
(885 6
x886 7
=>888 :
x88; <
.88< =
IsActive88= E
==88F H
true88I M
&&88N P
x88Q R
.88R S
StartDateTime88S `
<88a b
DateTime88c k
.88k l
Now88l o
)88o p
.88p q
Skip88q u
(88u v

startIndex	88v Ä
)
88Ä Å
.
88Å Ç
Take
88Ç Ü
(
88Ü á
rowSize
88á é
)
88é è
.
88è ê
ToList
88ê ñ
(
88ñ ó
)
88ó ò
;
88ò ô
_logger99 
.99 
Info99 
(99 
$str99 =
)99= >
;99> ?
return:: 
events:: 
.:: 
ToList:: 
(:: 
):: 
;:: 
};; 
publicCC 

ListCC 
<CC 

EventModelCC 
>CC 
GetUpComingEventsCC -
(CC- .
intCC. 1

startIndexCC2 <
,CC< =
intCC> A
rowSizeCCB I
)CCI J
{DD 
_loggerEE 
.EE 
InfoEE 
(EE 
$strEE G
)EEG H
;EEH I
ListFF 

<FF
 

EventModelFF 
>FF 
eventsFF 
=FF 
_contextFF  (
.FF( )
EventsFF) /
.FF/ 0
WhereFF0 5
(FF5 6
xFF6 7
=>FF8 :
xFF; <
.FF< =
IsActiveFF= E
==FFF H
trueFFI M
&&FFN P
xFFQ R
.FFR S
StartDateTimeFFS `
>FFa b
DateTimeFFc k
.FFk l
NowFFl o
)FFo p
.FFp q
SkipFFq u
(FFu v

startIndex	FFv Ä
)
FFÄ Å
.
FFÅ Ç
Take
FFÇ Ü
(
FFÜ á
rowSize
FFá é
)
FFé è
.
FFè ê
ToList
FFê ñ
(
FFñ ó
)
FFó ò
;
FFò ô
_loggerGG 
.GG 
InfoGG 
(GG 
$strGG Q
)GGQ R
;GGR S
returnHH 
eventsHH 
.HH 
ToListHH 
(HH 
)HH 
;HH 
}II 
publicPP 

boolPP 
CreateAttendeePP 
(PP 
ListPP #
<PP# $
EventAttendeeModelPP$ 6
>PP6 7
eventAttendeesPP8 F
)PPF G
{QQ 
_loggerRR 
.RR 
InfoRR 
(RR 
$strRR D
)RRD E
;RRE F
_contextSS 
.SS 
AddRangeSS 
(SS 
eventAttendeesSS &
)SS& '
;SS' (
_contextTT 
.TT 
SaveChangesTT 
(TT 
)TT 
;TT 
_loggerUU 
.UU 
InfoUU 
(UU 
$strUU X
,UUX Y
eventAttendeesUUZ h
)UUh i
;UUi j
returnVV 
trueVV 
;VV 
}WW 
public^^ 


EventModel^^ 
?^^ 
GetEventById^^ #
(^^# $
Guid^^$ (
eventId^^) 0
)^^0 1
{__ 
_logger`` 
.`` 
Info`` 
(`` 
$str`` 6
,``6 7
eventId``8 ?
)``? @
;``@ A

EventModelaa 
?aa 

eventModelaa 
=aa 
_contextaa '
.aa' (
Eventsaa( .
.aa. /
FirstOrDefaultaa/ =
(aa= >
xaa> ?
=>aa@ B
xaaC D
.aaD E
IdaaE G
==aaH J
eventIdaaK R
)aaR S
;aaS T
_loggerbb 
.bb 
Infobb 
(bb 
$strbb E
)bbE F
;bbF G
returncc 

eventModelcc 
;cc 
}dd 
publicll 

Listll 
<ll 
UserDtoll 
>ll 
GetConflictedUsersll +
(ll+ ,
DateTimell, 4
startDateTimell5 B
,llB C
DateTimellD L
endDateTimellM X
,llX Y
ListllZ ^
<ll^ _
UserDtoll_ f
>llf g
	attendeesllh q
)llq r
{mm 
_loggernn 
.nn 
Infonn 
(nn 
$strnn S
,nnS T
startDateTimennU b
,nnb c
endDateTimennd o
)nno p
;nnp q
varoo 	
usersoo
 
=oo 
fromoo 
eventsoo 
inoo  
_contextoo! )
.oo) *
Eventsoo* 0
.oo0 1
Whereoo1 6
(oo6 7
xoo7 8
=>oo9 ;
(oo< =
xoo= >
.oo> ?
StartDateTimeoo? L
>=ooM O
startDateTimeooP ]
&&oo^ `
xooa b
.oob c
StartDateTimeooc p
<=ooq s
endDateTimeoot 
)	oo Ä
||pp< >
(pp? @
xpp@ A
.ppA B
EndDateTimeppB M
>=ppN P
startDateTimeppQ ^
&&pp_ a
xppb c
.ppc d
EndDateTimeppd o
<=ppp r
endDateTimepps ~
)pp~ 
)	pp Ä
joinqq 
eventAttendeesqq %
inqq& (
_contextqq) 1
.qq1 2
EventAttendeesqq2 @
onrr 
eventsrr 
.rr  
Idrr  "
equalsrr# )
eventAttendeesrr* 8
.rr8 9
EventIdrr9 @
joinss 
userss 
inss 
_contextss '
.ss' (
Usersss( -
ontt 
eventAttendeestt '
.tt' (
UserIdtt( .
equalstt/ 5
usertt6 :
.tt: ;
UserIdtt; A
selectuu 
newuu 
UserDtouu $
{uu% &
UserIduu' -
=uu. /
useruu0 4
.uu4 5
UserIduu5 ;
,uu; <
UserNameuu= E
=uuF G
useruuH L
.uuL M
UserNameuuM U
}uuV W
;uuW X
_loggervv 
.vv 
Infovv 
(vv 
$strvv [
,vv[ \
usersvv] b
)vvb c
;vvc d
returnww 
usersww 
.ww 
ToListww 
(ww 
)ww 
;ww 
}xx 
public 

List 
< 

EventIdDto 
> 
GetEventsForUser ,
(, -
int- 0
userId1 7
)7 8
{
ÄÄ 
_logger
ÅÅ 
.
ÅÅ 
Info
ÅÅ 
(
ÅÅ 
$str
ÅÅ E
,
ÅÅE F
userId
ÅÅG M
)
ÅÅM N
;
ÅÅN O
var
ÇÇ 	
events
ÇÇ
 
=
ÇÇ 
from
ÇÇ 
eventAttendees
ÇÇ &
in
ÇÇ' )
_context
ÇÇ* 2
.
ÇÇ2 3
EventAttendees
ÇÇ3 A
.
ÇÇA B
Where
ÇÇB G
(
ÇÇG H
x
ÇÇH I
=>
ÇÇJ L
x
ÇÇM N
.
ÇÇN O
UserId
ÇÇO U
==
ÇÇV X
userId
ÇÇY _
)
ÇÇ_ `
join
ÉÉ 
e
ÉÉ 
in
ÉÉ 
_context
ÉÉ %
.
ÉÉ% &
Events
ÉÉ& ,
on
ÑÑ 
eventAttendees
ÑÑ &
.
ÑÑ& '
EventId
ÑÑ' .
equals
ÑÑ/ 5
e
ÑÑ6 7
.
ÑÑ7 8
Id
ÑÑ8 :
select
ÖÖ 
new
ÖÖ 

EventIdDto
ÖÖ (
{
ÖÖ) *
Id
ÖÖ+ -
=
ÖÖ. /
e
ÖÖ0 1
.
ÖÖ1 2
Id
ÖÖ2 4
,
ÖÖ4 5
	EventName
ÖÖ6 ?
=
ÖÖ@ A
e
ÖÖB C
.
ÖÖC D
	EventName
ÖÖD M
,
ÖÖM N
StartDateTime
ÖÖO \
=
ÖÖ] ^
e
ÖÖ_ `
.
ÖÖ` a
StartDateTime
ÖÖa n
,
ÖÖn o
EndDateTime
ÖÖp {
=
ÖÖ| }
e
ÖÖ~ 
.ÖÖ Ä
EndDateTimeÖÖÄ ã
}ÖÖå ç
;ÖÖç é
_logger
ÜÜ 
.
ÜÜ 
Info
ÜÜ 
(
ÜÜ 
$str
ÜÜ L
)
ÜÜL M
;
ÜÜM N
return
áá 
events
áá 
.
áá 
ToList
áá 
(
áá 
)
áá 
;
áá 
}
àà 
}
ââ 
}ää ˝/
GC:\Users\VishnuPadiga\Desktop\EventManager\Repository\UserRepository.cs
	namespace 	

Repository
 
{ 
public		 
class			 
UserRepository		 
:		 
IUserRepository		  /
{

 
private 
readonly 
ApiDbContext !
_context" *
;* +
private 
static 
readonly 
Logger "
_logger# *
=+ ,

LogManager- 7
.7 8!
GetCurrentClassLogger8 M
(M N
)N O
;O P
public 

UserRepository 
( 
ApiDbContext &
context' .
). /
{ 
_context 
= 
context 
; 
} 
public 

bool 

UploadUser 
( 
List 
<  
	UserModel  )
>) *
users+ 0
)0 1
{ 
_logger 
. 
Info 
( 
$str 7
,7 8
users9 >
)> ?
;? @
List 

<
 
	UserModel 
> 
newUsers 
=  
(! "
from" &
user' +
in, .
users/ 4
join" &
	userModel' 0
in1 3
_context4 <
.< =
Users= B
on" $
user% )
.) *
UserId* 0
equals1 7
	userModel8 A
.A B
UserIdB H
intoI M
newTableN V
from" &
newRow' -
in. 0
newTable1 9
.9 :
DefaultIfEmpty: H
(H I
)I J
where" '
newRow( .
==/ 1
null2 6
select  " (
new  ) ,
	UserModel  - 6
{!!" #
UserId""$ *
=""+ ,
user""- 1
.""1 2
UserId""2 8
,""8 9
UserName##$ ,
=##- .
user##/ 3
.##3 4
UserName##4 <
}$$" #
)$$# $
.$$$ %
ToList$$% +
($$+ ,
)$$, -
;$$- .
_logger%% 
.%% 
Debug%% 
(%% 
$str%% <
,%%< =
newUsers%%> F
,%%F G
users%%H M
)%%M N
;%%N O
_context'' 
.'' 
Users'' 
.'' 
AddRange'' 
('' 
newUsers'' &
)''& '
;''' (
_context(( 
.(( 
SaveChanges(( 
((( 
)(( 
;(( 
_logger** 
.** 
Info** 
(** 
$str** 7
,**7 8
users**9 >
)**> ?
;**? @
return++ 
true++ 
;++ 
},, 
public44 

List44 
<44 
	UserModel44 
>44 
GetAllUsers44 &
(44& '
int44' *

startIndex44+ 5
,445 6
int447 :
rowSize44; B
)44B C
{55 
List66 

<66
 
	UserModel66 
>66 
users66 
=66 
_context66 &
.66& '
Users66' ,
.66, -
Skip66- 1
(661 2

startIndex662 <
)66< =
.66= >
Take66> B
(66B C
rowSize66C J
)66J K
.66K L
ToList66L R
(66R S
)66S T
;66T U
return77 
users77 
;77 
}88 
public?? 

	UserModel?? 
??? 
GetUserById?? !
(??! "
int??" %
userId??& ,
)??, -
{@@ 
_loggerAA 
.AA 
InfoAA 
(AA 
$strAA <
,AA< =
userIdAA> D
)AAD E
;AAE F
	UserModelBB 
?BB 
userBB 
=BB 
_contextBB  
.BB  !
UsersBB! &
.BB& '
FirstOrDefaultBB' 5
(BB5 6
uBB6 7
=>BB8 :
uBB; <
.BB< =
UserIdBB= C
==BBD F
userIdBBG M
)BBM N
;BBN O
_loggerCC 
.CC 
InfoCC 
(CC 
$strCC E
,CCE F
userCCG K
)CCK L
;CCL M
returnDD 
userDD 
;DD 
}EE 
publicLL 

ListLL 
<LL 
UserDtoLL 
>LL 
GetUsersForEventLL )
(LL) *
GuidLL* .
eventIdLL/ 6
)LL6 7
{MM 
_loggerNN 
.NN 
InfoNN 
(NN 
$strNN >
,NN> ?
eventIdNN@ G
)NNG H
;NNH I
varOO 	
usersOO
 
=OO 
fromOO 
eventAttendeesOO %
inOO& (
_contextOO) 1
.OO1 2
EventAttendeesOO2 @
.OO@ A
WhereOOA F
(OOF G
xOOG H
=>OOI K
xOOL M
.OOM N
EventIdOON U
==OOV X
eventIdOOY `
)OO` a
joinPP 
userPP 
inPP 
_contextPP '
.PP' (
UsersPP( -
onQQ 
eventAttendeesQQ '
.QQ' (
UserIdQQ( .
equalsQQ/ 5
userQQ6 :
.QQ: ;
UserIdQQ; A
selectRR 
newRR 
UserDtoRR $
{RR% &
UserIdRR' -
=RR. /
userRR0 4
.RR4 5
UserIdRR5 ;
,RR; <
UserNameRR= E
=RRF G
userRRH L
.RRL M
UserNameRRM U
}RRV W
;RRW X
_loggerSS 
.SS 
InfoSS 
(SS 
$strSS Q
,SSQ R
eventIdSSS Z
)SSZ [
;SS[ \
returnTT 
usersTT 
.TT 
ToListTT 
(TT 
)TT 
;TT 
}UU 
}WW 
}XX 