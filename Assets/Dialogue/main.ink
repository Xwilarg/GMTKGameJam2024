/* 
GMTK Game Jam 2024
Theme: Built to Scale
Gameplay: Build robots based on customer commissions. Scale up robots and expand business.
Writing needs: Dialogue
*/

// Customers
VAR bot_number = 0
VAR bot_type = "cat"
//LIST bot_type = (builder), (cook), (firefighter), (cat), (cowboy), (fixer), (warrior)

-> mayor.game_intro
== mayor ==
= game_intro
~ bot_number = 1
~ bot_type = "cat"
Congratulations on your new shop! As mayor, I'm always eager to support small businesses in this town! # speaker mayor
Speaking of support, I need your help. # speaker mayor
City Hall is a stinking mess today - turns out a nest of rats has been living in our vents!  Could you send {bot_number} of your {bot_type}s to take care of the situation? # speaker mayor
-> DONE

= host_event
~ bot_number = RANDOM(3, 20)
~ bot_type = "cook"
Hello, your shop seems to be doing well! My office pushed for those tax incentives in the last election - we're big on helping businesses! # speaker mayor
Anyway, we're hosting a delegation from our sister city this week and our regular caterers are overbooked. Do you have a team of {bot_number} that can prepare {~Greek food|brown bag lunches|pizzas|BBQ ribs|sandwiches}?
-> DONE

== fire_chief ==
= forest_fire
~ bot_number = RANDOM(3, 20)
~ bot_type = "firefighter"
The forest fire is out of control and heading towards town. My people need all the help they can get. Give me {bot_number} of your best {bot_type}{bot_number>1:s|}! # speaker fire_chief
-> DONE

= zoo_escape
~ bot_number = RANDOM(3, 20)
~ bot_type = "firefighter, cowboy"
You haven't seen a tapir around, have you?! It's black and white, with a snout like an anteater. # speaker fire_chief
A fire broke out in the zoo, and half the animals escaped! I need help, {bot_number} of your best! # speaker fire_chief
-> DONE

== mafia ==
= turf_war
~ bot_number = RANDOM(3, 20)
~ bot_type = "warrior"
New business eh, bro? You should meet my boss Don Fusilli. You'll need his help one day. # speaker mafia
Don Fusilli takes care of his own. Question is, will you step up for the Don? The {~Ziti|Rigatoni|Gemelli|Farfalle|Cavatappi} are trying to steal our territory and Don Fusilli needs {bot_number} {bot_type}{bot_number>1:s|}. # speaker mafia
-> DONE

= wedding
~ bot_number = RANDOM(3, 20)
~ bot_type = "cook, warrior"
Bro, it's Don Fusilli's daughter Margherita's wedding next week. You should come by, you know, pay your respects and all. # speaker mafia
The Don also wants to send business your way. Send {bot_number} of your guys to guard or help with the wedding.
-> DONE

== rancher ==
= lost
~ bot_number = RANDOM(3, 20)
~ bot_type = "cowboy"
S'up amigo? Some of our cattle escaped and I sure could use some help tracking 'em down. You got {bot_number} {bot_type}{bot_number>1:s|} 'round these parts? # speaker rancher
-> DONE

= fence
~ bot_number = RANDOM(3, 20)
~ bot_type = "fixer, cook"
Hola, amigo! A few other ranch hands offered to help fix our fences. We probably need more help though, 'specially since we promised to feed them. # speaker rancher
Could use {bot_number} of yours on the day of! # speaker rancher
-> DONE

// Bots
VAR build_success = true
VAR found_job = 1

== guyd_bot ==
= tutorial
~ bot_number = RANDOM(10, 15)
~ bot_type = "builder"
Nicely done! That's why you're the boss, boss! # speaker guyd
A smart boss also knows how to delegate, wink, wink. Go ahead and make {bot_number} {bot_type}{bot_number>1:s|} - they'll help you build even more bots! # speaker guyd
-> DONE

= order_completed
Bot team complete and ready to roll! # speaker guyd
-> DONE

= wrong_bots
Someone doesn't belong on this team... # speaker guyd
-> DONE

= wrong_number
There aren't enough of you for this job. Or maybe there are too many of you? I can't decide... # speaker guyd
-> DONE

= report_task
Hmm, wonder what's going on in town? # speaker guyd
~ found_job = RANDOM(0, 1)
~ bot_number = RANDOM(3, 20)
//Oh, someone needs {bot_number} {LIST_RANDOM(bot_type)}{bot_number>1:s|}! Let's get on it, {bot_number} {bot_number>1:bots|bot} coming right up! # speaker guyd
-> DONE

= no_task
Quiet day, I guess. Slow day for business too... # speaker guyd
-> DONE

== builder_bot ==
{
  - build_success: -> success
  - else: -> try_again
}

= success
Ready to build, boss!
-> DONE 

= try_again
Uh-oh, are these the right schematics?
-> DONE

== cook_bot ==
{
  - build_success: -> can_cook
  - else: -> cannot_cook
}

= can_cook
I can make you {~baklava|lasagna|ice cream|chicken cordon bleu|sushi}!
-> DONE

= cannot_cook
Help! How do you boil water?
-> DONE

== cowboy_bot ==
{
  - build_success: -> rodeo
  - else: -> no_rodeo
}

= rodeo
This ain't my first rodeo. Giddy up!
-> DONE

= no_rodeo
Why am I riding a horse? 
-> DONE

== kitty_bot ==
{
  - build_success: -> is_cat
  - else: -> not_cat
}

= is_cat
Meow!
-> DONE

= not_cat
I don't hunt. Who do you think I am, a cat?
-> DONE

== fire_bot ==
{
  - build_success: -> hot
  - else: -> not_hot
}

= hot
There's no smoke without... FIRE! I'll take care of this!
-> DONE

= not_hot
Why am I smoking?! And I don't mean hot.
-> DONE

== battle_bot ==
{
  - build_success: -> fight
  - else: -> no_fight
}

= fight
Let me at 'em!
-> DONE

= no_fight
Can't we all just get along?
-> DONE

== repair_bot ==
{
  - build_success: -> fix
  - else: -> broken
}

= fix
I'll grab my {~hammer|wrench|screwdriver}. This'll take no time at all!
-> DONE

= broken
I might have a screw or two loose, but who doesn't, right am I?
-> DONE
