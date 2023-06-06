export default class Helper {
  static convertBase64toURI = (base64: string) => {
    const buffer = Buffer.from(base64, "base64");

    // Create a data URI for the SVG image
    const svgDataUri = `data:image/svg+xml;base64,${buffer.toString("base64")}`;
    return svgDataUri;
  };

  static parseJwt(token: string) {
    return JSON.parse(Buffer.from(token.split(".")[1], "base64").toString());
  }

  static getLinearGradient = (hexColors: string[]) => {
    let gradientStr = "";
    const percentageAmount = (100 / hexColors.length).toString() + "%";
    for (const color of hexColors) {
      gradientStr += color + " " + percentageAmount + ",";
    }
    const subStr = gradientStr.substring(0, gradientStr.length - 1);

    return (
      "linear-gradient(to right, " +
      subStr +
      (hexColors.length > 1 ? ")" : ",transparent)")
    );
  };

  static getLastSixMonths = () => {
    const months = [
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec",
    ];

    const lastSixMonths = [];

    for (let i = 0; i < 6; i++) {
      const d = new Date();
      d.setMonth(d.getMonth() - i);
      lastSixMonths.push(months[d.getMonth()]);
    }
    return lastSixMonths;
  };
}
