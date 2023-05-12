Õ
HC:\Users\VishnuPadiga\Desktop\EventManager\Contracts\IEventRepository.cs
	namespace 	

Repository
 
{ 
public 
	interface	 
IEventRepository #
{ 
bool 
CreateEvent	 
( 

EventModel 

eventModel  *
)* +
;+ ,
List 
< 	

EventModel	 
> 
GetAllEvents !
(! "
int" %

startIndex& 0
,0 1
int2 5
rowSize6 =
)= >
;> ?
List 
< 	

EventModel	 
> 
GetPastEvents "
(" #
int# &

startIndex' 1
,1 2
int3 6
rowSize7 >
)> ?
;? @
List'' 
<'' 	

EventModel''	 
>'' 
GetUpComingEvents'' &
(''& '
int''' *

startIndex''+ 5
,''5 6
int''7 :
rowSize''; B
)''B C
;''C D
bool.. 
CreateAttendee..	 
(.. 
List.. 
<.. 
EventAttendeeModel.. /
>../ 0
eventAttendees..1 ?
)..? @
;..@ A

EventModel55 
?55 
GetEventById55 
(55 
Guid55 !
eventId55" )
)55) *
;55* +
List== 
<== 	
UserDto==	 
>== 
GetConflictedUsers== $
(==$ %
DateTime==% -
startDateTime==. ;
,==; <
DateTime=== E
endDateTime==F Q
,==Q R
List==S W
<==W X
UserDto==X _
>==_ `
	attendees==a j
)==j k
;==k l
ListDD 
<DD 	

EventIdDtoDD	 
>DD 
GetEventsForUserDD %
(DD% &
intDD& )
userIdDD* 0
)DD0 1
;DD1 2
}FF 
}GG º
EC:\Users\VishnuPadiga\Desktop\EventManager\Contracts\IEventService.cs
	namespace 	
Services
 
{ 
public 
	interface	 
IEventService  
{ 
IdDto 	
CreateEvent
 
( 
EventDto 
eventDto '
)' (
;( )
List 
< 	

EventIdDto	 
> 
	GetEvents 
( 
string %
eventKey& .
,. /
int0 3

startIndex4 >
,> ?
int@ C
rowSizeD K
)K L
;L M
List   
<   	
UserDto  	 
>   
CreateAttendee    
(    !
Guid  ! %
eventId  & -
,  - .
	IFormFile  / 8
file  9 =
)  = >
;  > ?
List'' 
<'' 	
UserDto''	 
>'' 
GetUsersForEvent'' "
(''" #
Guid''# '
eventId''( /
)''/ 0
;''0 1
}(( 
})) Ÿ
GC:\Users\VishnuPadiga\Desktop\EventManager\Contracts\IUserRepository.cs
	namespace 	

Repository
 
{ 
public 
	interface	 
IUserRepository "
{ 
bool 

UploadUser	 
( 
List 
< 
	UserModel "
>" #
user$ (
)( )
;) *
List 
< 	
	UserModel	 
> 
GetAllUsers 
(  
int  #

startIndex$ .
,. /
int0 3
rowSize4 ;
); <
;< =
	UserModel 
? 
GetUserById 
( 
int 
userId %
)% &
;& '
List## 
<## 	
UserDto##	 
>## 
GetUsersForEvent## "
(##" #
Guid### '
eventId##( /
)##/ 0
;##0 1
}$$ 
}%% è
DC:\Users\VishnuPadiga\Desktop\EventManager\Contracts\IUserService.cs
	namespace 	
Services
 
{ 
public 
	interface	 
IUserService 
{ 
bool 

UploadUser	 
( 
	IFormFile 
file "
)" #
;# $
List 
< 	
UserDto	 
> 
GetAllUsers 
( 
int !

startIndex" ,
,, -
int. 1
rowSize2 9
)9 :
;: ;
UserDto 
GetUserById 
( 
int 
userId "
)" #
;# $
List## 
<## 	

EventIdDto##	 
>## 
GetEventsForUser## %
(##% &
int##& )
userId##* 0
)##0 1
;##1 2
}$$ 
}%% 