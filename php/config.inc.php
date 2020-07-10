<?php
// USE SMTP OR mail()
// SMTP is recommended, mail() is disabled on most shared hosting servers.
// IF false : SMTP host/port/user/pass/ssl not used, leave empty or as it is!
$config['use_smtp']				= true;						// true|false


// SMTP Server Settings
$config['smtp_host'] 			= 'ssl://smtp.gmail.com';   // eg.: smtp.mandrillapp.com
$config['smtp_port'] 			= 465;						// eg.: 587
$config['smtp_user'] 			= ''; 						// you@gmail.com
$config['smtp_pass'] 			= '';						// password
$config['smtp_ssl']				= false;					// should remain false

// Who receive all emails?
$config['send_to']				= 'you@gmail.com';			// destination of all emails sent throught contact form

// Email Subject
$config['subject']				= 'Contact Form';			// subject of emails you receive
?>