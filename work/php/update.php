<?php
/**
 * A page controller
 */
require "configure.php";
require "src/functions.php";

// Get incoming values
$item  = $_GET["item"] ?? null;
$id    = $_POST["id"] ?? null;
$label = $_POST["label"] ?? null;
$type  = $_POST["type"] ?? null;
$save  = $_POST["save"] ?? null;
// var_dump($_GET);
// var_dump($_POST);

$db = connectDatabase($dsn);

$sql = "SELECT * FROM tech";
$stmt = $db->prepare($sql);
$stmt->execute();
$res1 = $stmt->fetchAll();
//var_dump($res1);

if ($item) {
    $sql = "SELECT * FROM tech WHERE id = ?";
    $stmt = $db->prepare($sql);
    $stmt->execute([$item]);
    $res2 = $stmt->fetch();
    //var_dump($res2);
}

if ($save) {
    $sql = "UPDATE tech SET label = ?, type = ? WHERE id = ?";
    $stmt = $db->prepare($sql);
    $stmt->execute([$label, $type, $id]);
    //var_dump([$label, $type, $id]);

    header("Location: " . $_SERVER['PHP_SELF'] . "?item=$id");
    exit;
}
?>
<!doctype html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <title>schools in district</title>
        <link rel="stylesheet" href="css/phpTableStyle.css">
        <link rel="icon" href="favicon.ico">
        <script src="js/navigate.js" type="text/javascript"></script>
    </head>
    <body>
    <?php
        require "./view/header.php";   
    ?>  
    <h1>Update a row in the table</h1>

    <form>
        <select name="item" onchange="this.form.submit()">
            <option value="-1">Select item</option>

            <?php foreach ($res1 as $row) : ?>
                <option value="<?= $row["id"] ?>"><?= "(" . $row["id"]. ") " . $row["label"] ?></option>
            <?php endforeach; ?>

        </select>
    </form>


    <?php if ($res2 ?? null) : ?>
    <form method="post">
        <fieldset>
            <legend>Update</legend>
            <p>
                <label>Id: 
                    <input type="text" readonly="readonly" name="id" value="<?= $res2["id"] ?>">
                </label>
            </p>
            <p>
                <label>Label: 
                    <input type="text" name="label" value="<?= $res2["label"] ?>">
                </label>
            </p>
            <p>
                <label>Type: 
                    <input type="text" name="type" value="<?= $res2["type"] ?>">
                </label>
            </p>
            <p>
                <input type="submit" name="save" value="Save">
            </p>
        </fieldset>
    </form>
    <?php endif; ?>
        <?php
            require "./view/table.php";
            require "./view/footer.php";
        ?>
    </body>
</html>
