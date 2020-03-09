<?php
/**
 * A page controller
 */
require "configure.php";
require "src/functions.php";
// Get incoming values
$search = $_GET["search"] ?? null;


// Connect to the database
$db = connectDatabase($dsn);
$res = getAll($db);

?>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Presentation of my self in the course Web applications 2</title>
    <link rel="stylesheet" href="css/schoolStyle.css">
    <link rel="icon" href="favicon.ico">
</head>

<body>
    <!--
    Comments are written as HTML style.
    -->
<?php
require "./view/header.php";   
?>
<h1>Read the database</h1>
    
    <table cellpadding="5" style="border-collapse: collapse;">
        <thead>
            <tr>
                <th>ID</th>
                <th>Label</th>
                <th>Type</th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($res as $row) : ?>
                <tr>
                    <td><?= $row["id"] ?></td>
                    <td><?= $row["label"] ?></td>
                    <td><?= $row["type"] ?></td>         
                </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
    <?php
        require "./view/footer.php";
    ?>
</body>
</html>