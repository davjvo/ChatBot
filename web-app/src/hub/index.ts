import { HubConnectionBuilder } from "@microsoft/signalr";
import { Constants } from "../utils/constants";

const connection = new HubConnectionBuilder()
  .withUrl(`${Constants.BASE_URL}chathub`)
  .build();

export { connection };