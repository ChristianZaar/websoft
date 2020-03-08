<!doctype html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <title>schools in district</title>
        <link rel="stylesheet" href="css/schoolStyle.css">
        <link rel="icon" href="favicon.ico">
    </head>
    <body>
    <?php
        require "./view/header.php";   
    ?>  
        <div class = "selection-box">
            <span>Select a district.</span>
            <select id="districtSelect"></select>
        </div>
        <img src="img/duck.png" id = "duck-img" width="20%"/>
        <table>
            <thead>
                <tr>
                    <th>Skolenhetsnamn</th>
                    <th>Skolenhetskod</th>
                    <th>Kommunkod</th>
                    <th>PeOrgNr</th>
                </tr>
            </thead>
            <tbody id = table-body>
                <tr>
                    <td>...</td>
                    <td>...</td>
                    <td>...</td>
                    <td>...</td>
                </tr>
            </tbody>
        </table>
        <?php
            require "./view/footer.php";
        ?>
        <script type="text/javascript" src="js/fetch-shool.js"></script>
        <script type="text/javascript" src="js/duck.js"></script>
    </body>
</html>