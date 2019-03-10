DROP TABLE user_brewer;
DROP TABLE reviews;
DROP TABLE beers;
DROP TABLE breweries;
DROP TABLE user_admin;

create table breweries
(
	breweryID int identity(1,1) not null,
	name varchar(64) not null,
	address varchar(64) not null,
	city varchar(32) not null,
	state varchar(2) not null,
	phoneNumber varchar(14) not null,
	hours varchar(100) not null,
	description varchar(max) not null,

	constraint pk_brewery primary key (breweryID)
);

CREATE TABLE beers
(
	beerID int identity(1,1) not null,
	breweryID int not null,
	name varchar(32) not null,
	ABV int not null,
	IBU int not null,
	style varchar(32) not null,
	hintsOf varchar(64) not null,
	description varchar(max) not null,

	constraint pk_beer primary key (beerID),
	constraint fk_brewery foreign key (breweryID) references breweries (breweryID)

);

CREATE TABLE reviews
(
	reviewID int identity(1,1) not null,
	beerID int not null,
	subject varchar(100) not null,
	userName varchar(30) not null,
	reviewBody varchar(100) not null,
	rating int not null,
	
	constraint pk_review primary key (reviewId),
	constraint fk_review_beer foreign key (beerID) references beers (beerID)
);


CREATE TABLE user_brewer
(
	brewerID int identity(1,1) not null,
	breweryID int not null,
	userName varchar(32) not null,
	password varchar(30) not null,
	
	constraint pk_brewer primary key (brewerId),
	constraint fk_brewery_brewer foreign key (breweryID) references breweries (breweryID)
);

CREATE TABLE user_admin
(
	adminID int identity(1,1) not null,
	userName varchar(32) not null,
	password varchar(30) not null,
	
	constraint pk_admin primary key (adminId)
);



INSERT breweries VALUES ('Elevator Brewery and Draught House', '161 N. High Street', 'Columbus', 'OH', '(614)228.0500', 
'M-Th 11am - 11pm, F 11am - 12am, Sat 5pm - 12am, Sun Closed', 'SOMETHING IS ALWAYS GOING ON AT THE ELEVATOR - We are only a few blocks 
from the Blue Jackets Nationwide Arena, Palace Theatre, Capitol Square, The Greater Columbus Convention Center, the Short North District full of 
art galleries and shops and numerous hotels.Make the Elevator your stop before and after shows or hockey games! We invite you in to enjoy our beautiful 
historic bar and restaurant, and to savor any of our numerous handcrafted micro-brews. While you are here, grab a bite from our scrumptious lunch or dinner
menus and play a little pool or darts. We are proud to offer a unique and extensive wine and spirits list. The Elevator Brewery and Draught Haus proves to have something for everyone to enjoy.');

INSERT breweries VALUES ('Kindred Tasting Room and Beer Garden', '505 Morrison Rd', 'Gahanna', 'OH',  '(614)528.1227', 
'M-Th 4pm - 10pm, F 4pm - 12am, Sat 12pm - 12am, Sun 12pm - 8pm', 'From your seat in our Tasting Room you can see our Barrel House 
where we use historical brewing techniques to produce unique and flavorful ales. Through the use of wild yeast and oak barrel conditioning these beers 
gain added character and complexity. The beers could be toasty, fruit-forward or tart.  This is where our brewers can experiment and be creative while 
showing our love of time-honored tradition.');



INSERT INTO beers VALUES (1,'Uptown Pilsner',5.5,20,'Pre-prohibition Lager','fascism and despair', 'Created by German immigrants. This style includes the use of corn, which gives this crisp, hoppy lager a pleasant sweetness in the background.');
INSERT INTO beers VALUES (1,'Three Frogs IPA',7.6,80,'Session IPA','Citronella, Cardamum and Wasted time', 'Our IPA is dry-hopped for maximum flavor, but without the “hop overload” of many IPAs on the market; we always strive for a balanced flavor.');
INSERT INTO beers VALUES (1,'Bleeding Buckeye Red Ale',5.7,340,'Irish Red Ale','pretentious self worth', 'This is an American Red Ale, similar to an Irish Red, English ESB or American Amber Ale; it is well balanced, rich and a bit toasty, a perfect addition to any game day. GO BUCKS!');
INSERT INTO beers VALUES (1,'Dirty Dicks Nut Brown Ale',6.0,40,'Nut Brown','Kessel Spice', 'This mellow Brown Ale has hints of hazelnut; a rich satisfying body, with mild hop levels.');
INSERT INTO beers VALUES (1,'Dark Force Lager',5.2,58,'Dark Lager','Sith loathing', 'Lightly hopped German-style Dark Lager (dunkel in German) is smooth and rich, but not heavy or bitter, it has hints of nuts and cocoa. You’ll love it if you like dark beer – if you don’t normally like dark beer this will change your opinion and be a new favorite!');
INSERT INTO beers VALUES (2,'Hawaiian Shirt',4.2,15,'Blonde Ale','Miami and 80''s nostalgia', 'This American Blonde Ale has a fruity balance of Pineapple and Mango.');
INSERT INTO beers VALUES (2,'Frida',5.1,25,'Mexican Lager','Mariachi Salsa', 'Our mexican lager makes you want to dance the Cha-Cha.');
INSERT INTO beers VALUES (2,'Campfire',4.2,15,'Session Ale','Smoke and marshmallows', 'This Session Blonde Ale is like a cold night with a warm fire with friends.');
INSERT INTO beers VALUES (2,'Long Sleeves',6.5,80,'Red Rye IPA','flannel and teen angst', 'Curl up in front of a fire and enjoy 6 or 7.');

INSERT INTO reviews VALUES (1, 'Get Uptown Pilsner Downtown', 'C-Bus drinky', 'Best beer Downtown is Uptown Pilsner!!', 5);
INSERT INTO reviews VALUES (1, 'More like Throw-Up(town)', 'IPAistheWay', 'Garbage!! Still trying to get the flat taste out of my mouth', 1);
INSERT INTO reviews VALUES (4, 'Love that nut taste', 'SQRLCigars', 'Like liquid Walnut! Goes great with a nice cigar too!', 4);
INSERT INTO reviews VALUES (9, 'Pretty good but not smokey enough', 'pretentious palate', 'Could use more smokiness on the finish', 3);

INSERT INTO user_brewer VALUES (1, 'John', 'password');
INSERT INTO user_brewer VALUES (2, 'Steve', 'password');

INSERT INTO user_admin VALUES ('Admin', 'passbosh');