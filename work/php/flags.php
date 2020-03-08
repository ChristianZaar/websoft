<!doctype html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <title>About this site</title>
        <link rel="stylesheet" href="css/flagsStyle.css">
        <link rel="icon" href="favicon.ico">
    </head>
    <body>

        <?php
            require "./view/header.php";   
        ?>

        <div class="flag-box">
            <button  id="sweden-btn">Sweden</a>
            <button id="denmark-btn">Denmark</a>
            <button id="finland-btn">Finland</a>
        </div>

        <div class="flag-div" id="flagContainer">
            <div class="vertical-div" id="crossVertical"></div>
            <div class="horizontal-div" id="crossHorizontal"></div>
        </div> 
        <?php
            require "./view/footer.php";
        ?>
        <script type="text/javascript" src="js/flags.js"></script>
    </body>
</html>