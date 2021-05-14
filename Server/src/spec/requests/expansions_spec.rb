require 'rails_helper'

RSpec.describe "Expansions", type: :request do
  describe "GET /index" do
    it "returns http success" do
      get "/expansions"
      expect(response).to have_http_status(:success)
    end
  end

  describe "/draw" do
    before do
      @expansion = Expansion.create(bundle_name: "test", cost: 20)
      Card.create(expansion_id: @expansion.id, asset_name: "f001")
      Card.create(expansion_id: @expansion.id, asset_name: "f002")
      Card.create(expansion_id: @expansion.id, asset_name: "f003")
      Card.create(expansion_id: @expansion.id, asset_name: "f004")
      Card.create(expansion_id: @expansion.id, asset_name: "f005")
      param = {name: "test"}
      post "/login", params: param, as: :json
    end

    it "draw success" do
      param = {id: @expansion.id}
      post "/expansions/draw", params: param, as: :json
      res = JSON.parse(response.body)
      expect(res["result"]).to be_truthy
    end
    
    it "draw fail" do
      user = User.find_by(name: "test")
      user.stone = 0
      user.save()
      
      param = {id: @expansion.id}
      post "/expansions/draw", params: param, as: :json
      res = JSON.parse(response.body)
      expect(res["result"]).to be_falsey
    end
    
    it "invalid id" do
      param = {id: -1}
      post "/expansions/draw", params: param, as: :json
      expect(response).to have_http_status(304)
    end
    
  end
end
